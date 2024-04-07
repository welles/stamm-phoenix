namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

public record PublicEventModel
{
    public required string Name { get; init; }

    public required string Link { get; init; }

    public required DateOnly StartDate { get; init; }

    public DateOnly? EndDate { get; init; }
}
