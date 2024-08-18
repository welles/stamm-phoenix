using MediatR;

namespace StammPhoenix.Application.Commands.SeedDatabase;

public record SeedDatabaseCommand(string Email, string Password, string FirstName, string LastName, string? PhoneNumber, string? Address) : IRequest;
