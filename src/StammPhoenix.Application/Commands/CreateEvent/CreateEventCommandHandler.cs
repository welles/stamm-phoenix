using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Commands.CreateEvent;

[UsedImplicitly]
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Event>
{
    private IEventRepository EventRepository { get; }

    private IValidator<CreateEventCommand> Validator { get; }

    public CreateEventCommandHandler(IEventRepository eventRepository, IValidator<CreateEventCommand> validator)
    {
        EventRepository = eventRepository;
        Validator = validator;
    }

    public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var result = await this.EventRepository.AddEvent(request.Title, request.Link, request.IsPublic, request.StartDate, request.EndDate, request.Description, cancellationToken);

        return result;
    }
}
