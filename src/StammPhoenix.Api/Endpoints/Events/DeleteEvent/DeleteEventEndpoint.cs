using JetBrains.Annotations;
using MediatR;
using StammPhoenix.Api.Core;
using StammPhoenix.Domain.Enums;

namespace StammPhoenix.Api.Endpoints.Events.DeleteEvent;

[PublicAPI]
public class DeleteEventEndpoint : DeleteEndpoint<DeleteEventRequest, EventsGroup>
{
    private IMediator Mediator { get; }

    private IMapper Mapper { get; }

    public override string EndpointRoute => "/{Id}";

    public override string EndpointSummary => "Delete event with specified ID";

    public override string EndpointDescription => "Deletes the event with the specified ID";

    public override GroupDesignation[] EndpointRoles => [ GroupDesignation.Leitende ];

    public DeleteEventEndpoint(IMediator mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

    public override async Task HandleAsync(DeleteEventRequest req, CancellationToken ct)
    {
        var command = this.Mapper.DeleteEventRequestToDeleteEventCommand(req);

        await this.Mediator.Send(command, ct);
    }
}
