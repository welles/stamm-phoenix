namespace StammPhoenix.Application.Interfaces;

public interface IAppConfiguration
{
    /// <summary>
    /// The path where the application can put log files.
    /// </summary>
    public string LogPath { get; }

    /// <summary>
    /// The key used for signing JWT tokens by the application.
    /// </summary>
    public string SigningKey { get; }
}
