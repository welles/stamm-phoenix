using MediatR;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Enums;
using StammPhoenix.Domain.Exceptions;

namespace StammPhoenix.Application.Commands.SeedDatabase;

public sealed class SeedDatabaseCommandHandler(IDatabaseManager databaseManager, ILeaderRepository leaderRepository) : IRequestHandler<SeedDatabaseCommand>
{
    private IDatabaseManager DatabaseManager { get; } = databaseManager;

    private ILeaderRepository LeaderRepository { get; } = leaderRepository;

    public async Task Handle(SeedDatabaseCommand request, CancellationToken cancellationToken)
    {
        if (await this.DatabaseManager.CanConnectAsync(cancellationToken) &&
            (await this.LeaderRepository.GetLeaders(cancellationToken)).Count != 0 &&
            (await this.LeaderRepository.GetGroups(cancellationToken)).Count != 0)
        {
            throw new DatabaseAlreadyInitializedException();
        }

        await this.DatabaseManager.MigrateDatabaseAsync(cancellationToken);

        var admin = await this.LeaderRepository.CreateLeader(request.Email, request.FirstName, request.LastName, request.Password, request.PhoneNumber, request.Email, cancellationToken);

        var adminGroup = await this.LeaderRepository.CreateGroup("Admins", GroupDesignation.Admin, null, null, cancellationToken);

        await this.LeaderRepository.AddLeaderToGroup(admin.Id, adminGroup.Id, cancellationToken);

        var leaderGroup = await this.LeaderRepository.CreateGroup("Leitende", GroupDesignation.Leitende, null, null, cancellationToken);

        await this.LeaderRepository.AddLeaderToGroup(admin.Id, leaderGroup.Id, cancellationToken);
    }
}
