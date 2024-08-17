using MediatR;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Application.Commands.GetPublicEvents;

public record GetPublicEventsCommand(int Year) : IRequest<IReadOnlyCollection<Event>>;
