using MediatR;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Commands.GetEvents;

public class GetEventsCommandHandler : IRequestHandler<GetEventsCommand, IEnumerable<Event>>
{
    private IEventRepository EventRepository { get; }

    public GetEventsCommandHandler(IEventRepository eventRepository)
    {
        EventRepository = eventRepository;
    }

    public async Task<IEnumerable<Event>> Handle(GetEventsCommand request, CancellationToken cancellationToken)
    {
        return await this.EventRepository.GetEvents(cancellationToken);
    }
}
