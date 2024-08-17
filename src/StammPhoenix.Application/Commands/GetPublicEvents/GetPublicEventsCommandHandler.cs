using MediatR;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Commands.GetPublicEvents;

public class GetPublicEventsCommandHandler : IRequestHandler<GetPublicEventsCommand, IReadOnlyCollection<Event>>
{
    private IEventRepository EventRepository { get; }

    public GetPublicEventsCommandHandler(IEventRepository eventRepository)
    {
        EventRepository = eventRepository;
    }

    public async Task<IReadOnlyCollection<Event>> Handle(GetPublicEventsCommand request, CancellationToken cancellationToken)
    {
        return await this.EventRepository.GetPublicEventsForYear(request.Year, cancellationToken);
    }
}
