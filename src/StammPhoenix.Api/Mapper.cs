using Riok.Mapperly.Abstractions;
using StammPhoenix.Api.Endpoints.Events.DeleteEvent;
using StammPhoenix.Api.Endpoints.Events.PostEvent;
using StammPhoenix.Application.Commands.CreateEvent;
using StammPhoenix.Application.Commands.RemoveEvent;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Api;

[Mapper]
public partial class Mapper : IMapper
{
    public partial CreateEventCommand PostEventRequestToCreateEventCommand(PostEventRequest request);

    public partial PostEventResponse EventToPostEventResponse(Event createdEvent);

    public partial DeleteEventCommand DeleteEventRequestToDeleteEventCommand(DeleteEventRequest request);
}

public interface IMapper
{
    CreateEventCommand PostEventRequestToCreateEventCommand(PostEventRequest request);

    PostEventResponse EventToPostEventResponse(Event createdEvent);

    DeleteEventCommand DeleteEventRequestToDeleteEventCommand(DeleteEventRequest request);
}
