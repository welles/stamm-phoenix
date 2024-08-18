using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Interfaces;

public interface ILeaderRepository
{
    Task<IReadOnlyCollection<Leader>> GetLeaders(CancellationToken cancellationToken);

    Task<Leader> CreateLeader(string loginEmail, string firstName, string lastName, string password, string? phoneNumber, string? address);

    Task AddLeaderToGroup(Guid leaderId, Guid groupId);
}
