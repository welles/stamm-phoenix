using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Infrastructure.Configuration;

public class EnvironmentDatabaseConfiguration : IDatabaseConfiguration
{
    public static bool IsValid =>
        Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_HOST) != null
        && Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_PORT) != null
        && Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_NAME) != null
        && Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_USER) != null
        && Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_PASSWORD) != null;

    public EnvironmentDatabaseConfiguration()
    {
        var databaseHost = Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_HOST);

        if (string.IsNullOrWhiteSpace(databaseHost))
        {
            throw new ArgumentNullException(
                EnvironmentNames.DATABASE_HOST,
                "Environment variable must be set."
            );
        }

        var databasePortString = Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_PORT);

        if (
            string.IsNullOrWhiteSpace(databasePortString)
            || !int.TryParse(databasePortString, out var databasePort)
        )
        {
            throw new ArgumentNullException(
                EnvironmentNames.DATABASE_PORT,
                "Environment variable must be set."
            );
        }

        var databaseName = Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_NAME);

        if (string.IsNullOrWhiteSpace(databaseName))
        {
            throw new ArgumentNullException(
                EnvironmentNames.DATABASE_NAME,
                "Environment variable must be set."
            );
        }

        var databaseUser = Environment.GetEnvironmentVariable(EnvironmentNames.DATABASE_USER);

        if (string.IsNullOrWhiteSpace(databaseUser))
        {
            throw new ArgumentNullException(
                EnvironmentNames.DATABASE_USER,
                "Environment variable must be set."
            );
        }

        var databasePassword = Environment.GetEnvironmentVariable(
            EnvironmentNames.DATABASE_PASSWORD
        );

        if (string.IsNullOrWhiteSpace(databasePassword))
        {
            throw new ArgumentNullException(
                EnvironmentNames.DATABASE_PASSWORD,
                "Environment variable must be set."
            );
        }

        this.Host = databaseHost;
        this.Port = databasePort;
        this.Database = databaseName;
        this.User = databaseUser;
        this.Password = databasePassword;
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
