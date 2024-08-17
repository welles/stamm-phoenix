namespace StammPhoenix.Application.Interfaces;

public interface IDatabaseManager
{
    Task MigrateDatabaseAsync(CancellationToken cancellationToken);

    Task<bool> CanConnectAsync(CancellationToken cancellationToken);

    Task<IEnumerable<string>> GetPendingMigrationsAsync(CancellationToken cancellationToken);
}
