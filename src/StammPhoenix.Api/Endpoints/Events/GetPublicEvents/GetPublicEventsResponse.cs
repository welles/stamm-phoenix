namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

public record GetPublicEventsResponse
{
    public required int Year { get; init; }

    public required GetPublicEventsModel[] Events { get; init; }
}
