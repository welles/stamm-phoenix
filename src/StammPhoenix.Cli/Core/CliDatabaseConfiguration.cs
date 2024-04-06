using StammPhoenix.Application.Interfaces;
using StammPhoenix.Cli.Options.Database;

namespace StammPhoenix.Cli.Core;

public class CliDatabaseConfiguration : IDatabaseConfiguration
{
    public CliDatabaseConfiguration(DatabaseOptionsBase options)
    {
        this.Host = options.Host;
        this.Port = options.Port;
        this.Database = options.Database;
        this.User = options.User;
        this.Password = options.Password;
    }

    /// <inheritdoc />
    public string Host { get; }

    /// <inheritdoc />
    public int Port { get; }

    /// <inheritdoc />
    public string Database { get; }

    /// <inheritdoc />
    public string User { get; }

    /// <inheritdoc />
    public string Password { get; }
}
