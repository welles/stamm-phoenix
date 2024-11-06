using StammPhoenix.Domain.Enums;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Interfaces;

public interface ILeaderRepository
{
    Task<IReadOnlyCollection<Leader>> GetLeaders(CancellationToken cancellationToken);

    Task<Leader?> FindLeaderByEmail(string email, CancellationToken cancellationToken);

    Task<Leader> CreateLeader(
        string loginEmail,
        string firstName,
        string lastName,
        string password,
        string? phoneNumber,
        string? address,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyCollection<Group>> GetGroups(CancellationToken cancellationToken);

    Task<Group> CreateGroup(
        string name,
        GroupDesignation designation,
        string? meetingTime,
        string? meetingPlace,
        CancellationToken cancellationToken
    );

    Task AddLeaderToGroup(Guid leaderId, Guid groupId, CancellationToken cancellationToken);
}
