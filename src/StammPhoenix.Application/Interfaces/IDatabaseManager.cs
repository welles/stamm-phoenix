namespace StammPhoenix.Application.Interfaces;

public interface IDatabaseManager
{
    Task MigrateDatabaseAsync(CancellationToken cancellationToken);

    Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken);
}
