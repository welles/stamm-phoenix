namespace StammPhoenix.Api.Endpoints.Events.PostEvent;

public record PostEventRequest(
    string Title,
    string Link,
    bool IsPublic,
    DateOnly StartDate,
    DateOnly? EndDate,
    string? Description
);
