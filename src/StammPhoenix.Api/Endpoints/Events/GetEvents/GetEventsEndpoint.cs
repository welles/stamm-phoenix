using FastEndpoints;
using MediatR;
using StammPhoenix.Api.Core;
using StammPhoenix.Application.Commands.GetEvents;

namespace StammPhoenix.Api.Endpoints.Events.GetEvents;

public class GetEventsEndpoint : GetEndpoint<EmptyRequest, GetEventsResponse, EventsGroup>
{
    private IMediator Mediator { get; }

    private IMapper Mapper { get; }

    public override string EndpointRoute => string.Empty;

    public override string EndpointSummary => "Get a list of all events";

    public override string EndpointDescription => "Get a list of all events, even events that are not visible to the public";

    public override string[] EndpointRoles => [Domain.Core.Roles.Leader];

    public GetEventsEndpoint(IMediator mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

    public override async Task<GetEventsResponse> ExecuteAsync(EmptyRequest req, CancellationToken ct)
    {
        var command = new GetEventsCommand();

        var result = await this.Mediator.Send(command, ct);

        var models = result.Select(x => this.Mapper.EventToGetEventsModel(x) ).ToList();

        var response = new GetEventsResponse(models);

        return response;
    }
}
