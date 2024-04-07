namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

public record GetPublicEventsRequest
{
    public int? Year { get; init; }
}
