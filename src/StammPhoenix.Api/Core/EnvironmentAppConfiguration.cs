using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Api.Core;

public class Environment
{
    public static Variables GetVariables()
    {
        var logPath = System.Environment.GetEnvironmentVariable(Names.LOG_PATH);

        if (string.IsNullOrWhiteSpace(logPath))
        {
            throw new ArgumentNullException(Names.LOG_PATH, "Environment variable must be set.");
        }

        var signingKey = System.Environment.GetEnvironmentVariable(Names.SIGNING_KEY);

        if (string.IsNullOrWhiteSpace(signingKey))
        {
            throw new ArgumentNullException(Names.SIGNING_KEY, "Environment variable must be set.");
        }

        return new Variables
        {
            LogPath = logPath,
            SigningKey = signingKey
        };
    }

    public record Variables : IAppConfiguration
    {
        public required string LogPath { get; init; }

        public required string SigningKey { get; init; }
    }

    private static class Names
    {
        public const string LOG_PATH = nameof(Names.LOG_PATH);

        public const string SIGNING_KEY = nameof(Names.SIGNING_KEY);
    }
}
