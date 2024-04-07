namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

public record GetPublicEventsResponse
{
    public required int Year { get; init; }

    public required int Count { get; init; }

    public required PublicEventModel[] Events { get; init; }
}
