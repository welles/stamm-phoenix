using MediatR;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Commands.GetEvents;

public record GetEventsCommand : IRequest<IEnumerable<Event>>;
