namespace StammPhoenix.Api.Core;

public static class Environment
{
    public static Variables GetVariables()
    {
        var logPath = System.Environment.GetEnvironmentVariable(Names.LOG_PATH);

        if (string.IsNullOrWhiteSpace(logPath))
        {
            throw new ArgumentNullException(Names.LOG_PATH, "Environment variable must be set.");
        }

        return new Variables
        {
            LogPath = logPath
        };
    }

    public record Variables
    {
        public required string LogPath { get; init; }
    }

    private static class Names
    {
        public const string LOG_PATH = nameof(LOG_PATH);
    }
}
