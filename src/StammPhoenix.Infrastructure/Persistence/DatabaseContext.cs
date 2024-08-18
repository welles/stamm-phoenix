using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Enums;
using StammPhoenix.Domain.Exceptions;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Infrastructure.Persistence;

public sealed class DatabaseContext : DbContext, IDatabaseManager, ILeaderRepository, IEventRepository
{
    public DatabaseContext(IDatabaseConfiguration databaseConfiguration, IEnumerable<ISaveChangesInterceptor> saveChangesInterceptors, IPasswordHasher passwordHasher, ICurrentUser currentUser)
    {
        this.DatabaseConfiguration = databaseConfiguration;
        this.Interceptors = saveChangesInterceptors.OfType<IInterceptor>().ToArray();
        this.PasswordHasher = passwordHasher;
        this.CurrentUser = currentUser;
    }

    private IDatabaseConfiguration DatabaseConfiguration { get; }

    private IPasswordHasher PasswordHasher { get; }

    private ICurrentUser CurrentUser { get; }

    private IInterceptor[] Interceptors { get; }

    [PublicAPI]
    private DbSet<Leader> Leaders => this.Set<Leader>();

    [PublicAPI]
    private DbSet<Event> Events => this.Set<Event>();

    [PublicAPI]
    private DbSet<Group> Groups => this.Set<Group>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(this.Interceptors);

        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = this.DatabaseConfiguration.Host,
            Port =  this.DatabaseConfiguration.Port,
            Database = this.DatabaseConfiguration.Database,
            Username = this.DatabaseConfiguration.User,
            Password = this.DatabaseConfiguration.Password
        };

        optionsBuilder
            .UseNpgsql(connectionString.ConnectionString)
            .UseSnakeCaseNamingConvention();
    }

    public async Task MigrateDatabaseAsync(CancellationToken cancellationToken)
    {
        await this.Database.MigrateAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetPendingMigrationsAsync(CancellationToken cancellationToken)
    {
        return await this.Database.GetPendingMigrationsAsync(cancellationToken);
    }

    public async Task<bool> CanConnectAsync(CancellationToken cancellationToken)
    {
        return await this.Database.CanConnectAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Leader>> GetLeaders(CancellationToken cancellationToken)
    {
        return (await this.Leaders.ToArrayAsync(cancellationToken)).AsReadOnly();
    }

    public async Task<Leader> CreateLeader(string loginEmail, string firstName, string lastName, string password, string? phoneNumber,
        string? address)
    {
        if (this.Leaders.Any(x => x.LoginEmail == loginEmail))
        {
            throw new LeaderAlreadyExistsException(loginEmail);
        }

        var leader = new Leader
        {
            LoginEmail = loginEmail,
            FirstName = firstName,
            LastName = lastName,
            PasswordHash = this.PasswordHasher.HashPassword(password),
            PhoneNumber = phoneNumber,
            Address = address,

            Id = Guid.Empty,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = this.CurrentUser.Name,
            LastModifiedAt = DateTimeOffset.UtcNow,
            LastModifiedBy = this.CurrentUser.Name
        };

        var leaderResult = await this.Leaders.AddAsync(leader);

        await this.SaveChangesAsync();

        return leaderResult.Entity;
    }

    public async Task<Group> CreateGroup(string name, GroupDesignation designation, string? meetingTime, string? meetingPlace,
        CancellationToken cancellationToken)
    {
        if (this.Groups.Any(x => x.Designation == designation))
        {
            throw new GroupWithDesignationAlreadyExistsException(designation);
        }

        var newGroup = new Group
        {
            Name = name,
            Designation = designation,
            MeetingTime = meetingTime,
            MeetingPlace = meetingPlace,

            Id = Guid.Empty,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = this.CurrentUser.Name,
            LastModifiedAt = DateTimeOffset.UtcNow,
            LastModifiedBy = this.CurrentUser.Name
        };

        var groupResult = await this.Groups.AddAsync(newGroup, cancellationToken);

        await this.SaveChangesAsync(cancellationToken);

        return groupResult.Entity;
    }

    public async Task AddLeaderToGroup(Guid leaderId, Guid groupId)
    {
        var group = await this.Groups.FindAsync(groupId);

        if (group == null)
        {
            throw new GroupNotFoundException(groupId);
        }

        var leader = await this.Leaders.FindAsync(leaderId);

        if (leader == null)
        {
            throw new LeaderNotFoundException(leaderId);
        }

        group.AddMember(leader);

        await this.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Event>> GetEvents(CancellationToken ct)
    {
        return (await this.Events.ToArrayAsync(ct)).AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Event>> GetPublicEventsForYear(int year, CancellationToken ct)
    {
        return (await this.Events.Where(x => x.StartDate.Year == year && x.Public).ToListAsync(ct)).AsReadOnly();
    }

    public async Task<Event> AddEvent(string title, string link, bool isPublic, DateOnly startDate, DateOnly? endDate, string? description, CancellationToken ct)
    {
        if (this.Events.Any(x => x.Title == title && x.StartDate.Year == startDate.Year))
        {
            throw new EventAlreadyExistsException(title, startDate);
        }

        if (this.Events.Any(x => x.Link == link))
        {
            throw new EventLinkAlreadyExistsException(link);
        }

        var newEvent = new Event
        {
            Title = title,
            Link = link,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            Public = isPublic,

            Id = Guid.NewGuid(),
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = this.CurrentUser.Name,
            LastModifiedAt = DateTimeOffset.UtcNow,
            LastModifiedBy = this.CurrentUser.Name
        };

        var eventResult = await this.Events.AddAsync(newEvent, ct);

        await this.SaveChangesAsync(ct);

        return eventResult.Entity;
    }

    public async Task DeleteEvent(Guid id, CancellationToken ct)
    {
        var eventToDelete = await this.Events.FindAsync(id, ct);

        if (eventToDelete == null)
        {
            throw new EventNotFoundException(id);
        }

        this.Events.Remove(eventToDelete);

        await this.SaveChangesAsync(ct);
    }
}
