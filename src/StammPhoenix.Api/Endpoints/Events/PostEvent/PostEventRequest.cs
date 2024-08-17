namespace StammPhoenix.Api.Endpoints.Events.PostEvent;

public record PostEventRequest(string Title, string Link, DateOnly StartDate, DateOnly? EndDate, string? Description);
