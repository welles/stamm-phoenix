namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

public record GetPublicEventsModel(Guid Id, string Title, string Link, DateOnly StartDate, DateOnly? EndDate, string? Description);
