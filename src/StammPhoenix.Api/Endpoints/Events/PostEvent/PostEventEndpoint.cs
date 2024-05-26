using MediatR;
using StammPhoenix.Api.Core;

namespace StammPhoenix.Api.Endpoints.Events.PostEvent;

public sealed class PostEventEndpoint : PostEndpoint<PostEventRequest, PostEventResponse, EventsGroup>
{
    private IMediator Mediator { get; }

    public PostEventEndpoint(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    public override string EndpointRoute { get; } = "/";

    public override string EndpointSummary { get; } = "Add a new event";

    public override string EndpointDescription { get; } = "Adds a new event to the list of events, public or private";

    public override string[] EndpointRoles { get; } = [Domain.Core.Roles.Leader];

    public override Task<PostEventResponse> ExecuteAsync(PostEventRequest req, CancellationToken ct)
    {
        // TODO
        throw new NotImplementedException();
    }
}
