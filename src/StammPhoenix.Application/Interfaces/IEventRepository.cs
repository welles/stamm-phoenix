using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Interfaces;

public interface IEventRepository
{
    Task<Event> AddEvent(string title, string link, DateOnly startDate, DateOnly? endDate, string? description, CancellationToken ct);
}
