using MediatR;
using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Application.Commands.MigrateDatabase;

public class MigrateDatabaseCommandHandler(IDatabaseManager databaseManager) : IRequestHandler<MigrateDatabaseCommand>
{
    private IDatabaseManager DatabaseManager { get; } = databaseManager;

    public async Task Handle(MigrateDatabaseCommand request, CancellationToken cancellationToken)
    {
        if (await this.DatabaseManager.EnsureCreatedAsync(cancellationToken) == false)
        {
            throw new InvalidOperationException("Database does not exist");
        }

        await this.DatabaseManager.MigrateDatabaseAsync(cancellationToken);
    }
}
