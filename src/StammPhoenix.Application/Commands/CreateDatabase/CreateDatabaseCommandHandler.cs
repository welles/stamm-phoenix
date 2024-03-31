using MediatR;
using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Application.Commands.CreateDatabase;

public class CreateDatabaseCommandHandler(IDatabaseManager databaseManager)
    : IRequestHandler<CreateDatabaseCommand, bool>
{
    private IDatabaseManager DatabaseManager { get; } = databaseManager;

    public async Task<bool> Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
    {
        if (await this.DatabaseManager.EnsureCreatedAsync(cancellationToken) == false)
        {
            await this.DatabaseManager.MigrateDatabaseAsync(cancellationToken);

            return true;
        }

        return false;
    }
}
