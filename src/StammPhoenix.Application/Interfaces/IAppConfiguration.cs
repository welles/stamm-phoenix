namespace StammPhoenix.Application.Interfaces;

public interface IAppConfiguration
{
    /// <summary>
    /// The path where the application can put log files.
    /// </summary>
    public string LogPath { get; }

    /// <summary>
    /// The path where the application can put config files.
    /// </summary>
    public string ConfigPath { get; }

    /// <summary>
    /// The public key used for signing JWT tokens by the application.
    /// </summary>
    public string PublicSigningKey { get; }

    /// <summary>
    /// The private key used for signing JWT tokens by the application.
    /// </summary>
    public string PrivateSigningKey { get; }
}
