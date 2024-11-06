using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Core;

namespace StammPhoenix.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUser currentUser;

    public AuditableEntityInterceptor(ICurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        this.UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        this.UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (
                entry.State is EntityState.Added or EntityState.Modified
                || entry.HasChangedOwnedEntities()
            )
            {
                var utcNow = DateTimeOffset.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = this.currentUser.Name ?? "Unknown";
                    entry.Entity.CreatedAt = utcNow;
                }
                entry.Entity.LastModifiedBy = this.currentUser.Name ?? "Unknown";
                entry.Entity.LastModifiedAt = utcNow;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null
            && r.TargetEntry.Metadata.IsOwned()
            && (
                r.TargetEntry.State == EntityState.Added
                || r.TargetEntry.State == EntityState.Modified
            )
        );
}
