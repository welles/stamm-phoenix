﻿using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Infrastructure.Persistence;

public sealed class DatabaseContext : DbContext, IDatabaseManager
{
    public DatabaseContext(IDatabaseConfiguration databaseConfiguration, IEnumerable<ISaveChangesInterceptor> saveChangesInterceptors)
    {
        this.DatabaseConfiguration = databaseConfiguration;
        this.Interceptors = saveChangesInterceptors.OfType<IInterceptor>().ToArray();
    }

    private IDatabaseConfiguration DatabaseConfiguration { get; }

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
}
