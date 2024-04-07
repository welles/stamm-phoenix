using JetBrains.Annotations;
using StammPhoenix.Api.Core;

namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

[PublicAPI]
public sealed class GetPublicEventsEndpoint : GetEndpoint<GetPublicEventsRequest, GetPublicEventsResponse, EventsGroup>
{
    public override string EndpointRoute { get; } = "/public/{Year}";

    public override string EndpointSummary { get; } = "Get public events";

    public override string EndpointDescription { get; } =
        "Gets a list of events that have been released to the public";

    public override async Task<GetPublicEventsResponse> ExecuteAsync(GetPublicEventsRequest req, CancellationToken ct)
    {
        await Task.Delay(2000);

        return await Task.FromResult(new GetPublicEventsResponse
        {
            Year = req.Year!.Value,
            Count = 2,
            Events = new[]
            {
                new PublicEventModel
                {
                    Name = $"Stammeslager {req.Year}",
                    Link = $"stammeslager_{req.Year}",
                    StartDate = DateOnly.Parse($"{req.Year}-01-01"),
                    EndDate = DateOnly.Parse($"{req.Year}-01-07"),
                },
                new PublicEventModel
                {
                    Name = $"Hüttenwochenende {req.Year}",
                    Link = $"huettenwochenende_{req.Year}",
                    StartDate = DateOnly.Parse($"{req.Year}-10-15"),
                    EndDate = DateOnly.Parse($"{req.Year}-10-18"),
                }
            }
        });
    }
}
