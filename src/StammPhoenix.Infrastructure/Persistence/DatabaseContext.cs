using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Infrastructure.Persistence;

public sealed class DatabaseContext : DbContext
{
    [PublicAPI]
    private DbSet<Leader> Leaders => Set<Leader>();

    [PublicAPI]
    private DbSet<Event> Events => Set<Event>();

    [PublicAPI]
    private DbSet<Group> Groups => Set<Group>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
