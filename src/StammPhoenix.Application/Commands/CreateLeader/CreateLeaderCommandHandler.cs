using MediatR;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Commands.CreateLeader;

public class CreateLeaderCommandHandler : IRequestHandler<CreateLeaderCommand, Leader>
{
    private ILeaderRepository LeaderRepository { get; }

    public CreateLeaderCommandHandler(ILeaderRepository leaderRepository)
    {
        this.LeaderRepository = leaderRepository;
    }

    public async Task<Leader> Handle(CreateLeaderCommand request, CancellationToken cancellationToken)
    {
        return await this.LeaderRepository.CreateLeader(request.LoginEmail, request.FirstName, request.LastName,
            request.LoginPassword, request.PhoneNumber, request.Address);
    }
}
