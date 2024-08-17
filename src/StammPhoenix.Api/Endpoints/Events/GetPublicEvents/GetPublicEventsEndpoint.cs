using JetBrains.Annotations;
using MediatR;
using StammPhoenix.Api.Core;

namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

[PublicAPI]
public sealed class GetPublicEventsEndpoint : GetEndpoint<GetPublicEventsRequest, GetPublicEventsResponse, EventsGroup>
{
    private IMediator Mediator { get; }

    private IMapper Mapper { get; }

    public override string EndpointRoute { get; } = "/public/{Year}";

    public override string EndpointSummary { get; } = "Get public events";

    public override string EndpointDescription { get; } =
        "Gets a list of events that have been released to the public";

    public GetPublicEventsEndpoint(IMediator mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

    public override async Task<GetPublicEventsResponse> ExecuteAsync(GetPublicEventsRequest req, CancellationToken ct)
    {
        var command = this.Mapper.GetPublicEventsRequestToGetPublicEventsCommand(req);

        var result = await this.Mediator.Send(command, ct);

        var models = result.Select(x => this.Mapper.EventToGetPublicEventsModel(x)).ToArray();

        return new GetPublicEventsResponse
        {
            Year = req.Year,
            Events = models
        };
    }
}
