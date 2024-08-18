using Riok.Mapperly.Abstractions;
using StammPhoenix.Api.Endpoints.Events.DeleteEvent;
using StammPhoenix.Api.Endpoints.Events.GetEvents;
using StammPhoenix.Api.Endpoints.Events.GetPublicEvents;
using StammPhoenix.Api.Endpoints.Events.PostEvent;
using StammPhoenix.Api.Endpoints.Meta.Init;
using StammPhoenix.Application.Commands.CreateEvent;
using StammPhoenix.Application.Commands.GetPublicEvents;
using StammPhoenix.Application.Commands.RemoveEvent;
using StammPhoenix.Application.Commands.SeedDatabase;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Api;

[Mapper]
public partial class Mapper : IMapper
{
    public partial CreateEventCommand PostEventRequestToCreateEventCommand(PostEventRequest request);

    public partial PostEventResponse EventToPostEventResponse(Event createdEvent);

    public partial DeleteEventCommand DeleteEventRequestToDeleteEventCommand(DeleteEventRequest request);

    public partial GetEventsModel EventToGetEventsModel(Event eventItem);

    public partial GetPublicEventsCommand GetPublicEventsRequestToGetPublicEventsCommand(GetPublicEventsRequest request);

    public partial GetPublicEventsModel EventToGetPublicEventsModel(Event eventItem);

    public partial SeedDatabaseCommand InitRequestToSeedDatabaseCommand(InitRequest request);
}

public interface IMapper
{
    CreateEventCommand PostEventRequestToCreateEventCommand(PostEventRequest request);

    PostEventResponse EventToPostEventResponse(Event createdEvent);

    DeleteEventCommand DeleteEventRequestToDeleteEventCommand(DeleteEventRequest request);

    GetEventsModel EventToGetEventsModel(Event eventItem);

    GetPublicEventsCommand GetPublicEventsRequestToGetPublicEventsCommand(GetPublicEventsRequest request);

    GetPublicEventsModel EventToGetPublicEventsModel(Event eventItem);

    SeedDatabaseCommand InitRequestToSeedDatabaseCommand(InitRequest request);
}
