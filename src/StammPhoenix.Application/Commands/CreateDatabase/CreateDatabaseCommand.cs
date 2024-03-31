using MediatR;

namespace StammPhoenix.Application.Commands.CreateDatabase;

public record CreateDatabaseCommand : IRequest<bool>;
