namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

public record GetPublicEventsRequest
{
    public required int Year { get; init; }
}
