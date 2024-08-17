namespace StammPhoenix.Api.Endpoints.Events.PostEvent;

public record PostEventResponse(Guid Id, string Title, string Link, DateOnly StartDate, DateOnly? EndDate, string? Description);
