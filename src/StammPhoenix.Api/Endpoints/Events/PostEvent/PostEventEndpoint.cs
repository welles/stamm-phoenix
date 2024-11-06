using MediatR;
using StammPhoenix.Api.Core;
using StammPhoenix.Domain.Enums;

namespace StammPhoenix.Api.Endpoints.Events.PostEvent;

public sealed class PostEventEndpoint
    : PostEndpoint<PostEventRequest, PostEventResponse, EventsGroup>
{
    private IMediator Mediator { get; }

    private IMapper Mapper { get; }

    public PostEventEndpoint(IMediator mediator, IMapper mapper)
    {
        this.Mediator = mediator;
        Mapper = mapper;
    }

    public override string EndpointRoute { get; } = string.Empty;

    public override string EndpointSummary { get; } = "Add a new event";

    public override string EndpointDescription { get; } =
        "Adds a new event to the list of events, public or private";

    public override GroupDesignation[] EndpointRoles { get; } = [GroupDesignation.Leitende];

    public override async Task<PostEventResponse> ExecuteAsync(
        PostEventRequest req,
        CancellationToken ct
    )
    {
        var command = this.Mapper.PostEventRequestToCreateEventCommand(req);

        var result = await this.Mediator.Send(command, ct);

        return this.Mapper.EventToPostEventResponse(result);
    }
}
