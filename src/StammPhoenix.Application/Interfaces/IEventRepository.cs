using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Interfaces;

public interface IEventRepository
{
    Task<IReadOnlyCollection<Event>> GetEvents(CancellationToken ct);

    Task<Event> AddEvent(string title, string link, bool isPublic, DateOnly startDate, DateOnly? endDate, string? description, CancellationToken ct);

    Task DeleteEvent(Guid id, CancellationToken ct);
}
