namespace StammPhoenix.Api.Endpoints.Events.PostEvent;

public record PostEventRequest(string Name, string Link, DateOnly StartDate, DateOnly? EndDate);
