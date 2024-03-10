using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Api.Core;

public class EnvironmentAppConfiguration : IAppConfiguration
{
    public EnvironmentAppConfiguration()
    {
        var logPath = Environment.GetEnvironmentVariable(Names.LOG_PATH);

        if (string.IsNullOrWhiteSpace(logPath))
        {
            throw new ArgumentNullException(Names.LOG_PATH, "Environment variable must be set.");
        }

        var signingKey = Environment.GetEnvironmentVariable(Names.SIGNING_KEY);

        if (string.IsNullOrWhiteSpace(signingKey))
        {
            throw new ArgumentNullException(Names.SIGNING_KEY, "Environment variable must be set.");
        }

        this.LogPath = logPath;
        this.SigningKey = signingKey;
    }

    public static class Names
    {
        public const string LOG_PATH = nameof(Names.LOG_PATH);

        public const string SIGNING_KEY = nameof(Names.SIGNING_KEY);
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string LogPath { get; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string SigningKey { get; }
}
