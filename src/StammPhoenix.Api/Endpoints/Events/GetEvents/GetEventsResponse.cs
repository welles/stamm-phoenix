using StammPhoenix.Domain.Models;

namespace StammPhoenix.Api.Endpoints.Events.GetEvents;

public record GetEventsResponse(IEnumerable<GetEventsModel> Events);
