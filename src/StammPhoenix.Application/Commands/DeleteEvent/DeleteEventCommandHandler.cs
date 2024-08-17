using MediatR;
using StammPhoenix.Application.Commands.RemoveEvent;
using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Application.Commands.DeleteEvent;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private IEventRepository EventRepository { get; }

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        EventRepository = eventRepository;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        await this.EventRepository.DeleteEvent(request.Id, cancellationToken);
    }
}
