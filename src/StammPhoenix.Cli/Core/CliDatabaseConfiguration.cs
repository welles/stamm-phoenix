using StammPhoenix.Application.Interfaces;
using StammPhoenix.Cli.Options.Database;

namespace StammPhoenix.Cli.Core;

public class CliDatabaseConfiguration : IDatabaseConfiguration
{
    public CliDatabaseConfiguration(string host, int port, string database, string user, string password)
    {
        this.Host = host;
        this.Port = port;
        this.Database = database;
        this.User = user;
        this.Password = password;
    }

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
