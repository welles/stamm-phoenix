using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Infrastructure.Persistence;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

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
}
