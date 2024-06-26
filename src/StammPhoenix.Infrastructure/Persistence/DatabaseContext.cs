﻿using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Exceptions;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Infrastructure.Persistence;

public sealed class DatabaseContext : DbContext, IDatabaseManager, ILeaderRepository
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

    public async Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken)
    {
        return await this.Database.EnsureCreatedAsync(cancellationToken);
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
}
