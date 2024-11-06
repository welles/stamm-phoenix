namespace StammPhoenix.Api.Endpoints.Events.GetEvents;

public record GetEventsModel(
    Guid Id,
    string Title,
    string Link,
    DateOnly StartDate,
    DateOnly? EndDate,
    string? Description
);
