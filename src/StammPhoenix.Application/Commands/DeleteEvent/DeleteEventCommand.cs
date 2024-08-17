using MediatR;

namespace StammPhoenix.Application.Commands.RemoveEvent;

public class DeleteEventCommand : IRequest
{
    public required Guid Id { get; set; }
}
