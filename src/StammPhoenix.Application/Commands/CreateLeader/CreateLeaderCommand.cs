using MediatR;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Commands.CreateLeader;

public record CreateLeaderCommand : IRequest<Leader>
{
    public required string LoginEmail { get; set; }

    public required string LoginPassword { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
