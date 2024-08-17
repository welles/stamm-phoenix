using MediatR;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Commands.CreateEvent;

public class CreateEventCommand : IRequest<Event>
{
    public required string Title { get; set; }

    public required string Link { get; set; }

    public required bool IsPublic { get; set; }

    public required DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Description { get; set; }
}
