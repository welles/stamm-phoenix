using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Infrastructure.Persistence;

public class RuntimeDatabaseConfiguration : IDatabaseConfiguration
{
    public required string Host { get; init; }

    public required int Port { get; init; }

    public required string Database { get; init; }

    public required string User { get; init; }

    public required string Password { get; init; }
}
