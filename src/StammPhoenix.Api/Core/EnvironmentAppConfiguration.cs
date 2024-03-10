using System.Security.Cryptography;
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

        var configPath = Environment.GetEnvironmentVariable(Names.CONFIG_PATH);

        if (string.IsNullOrWhiteSpace(configPath))
        {
            throw new ArgumentNullException(Names.CONFIG_PATH, "Environment variable must be set.");
        }

        this.LogPath = logPath;
        this.ConfigPath = configPath;

        var publicKeyPath = (Path.Combine(configPath, "JWT.pub"));
        var privateKeyPath = (Path.Combine(configPath, "JWT.key"));
        if (File.Exists(publicKeyPath) && File.Exists(privateKeyPath))
        {
            this.PublicSigningKey = File.ReadAllText(publicKeyPath);
            this.PrivateSigningKey = File.ReadAllText(privateKeyPath);
        }
        else
        {
            var rsa = RSA.Create(4096);
            this.PublicSigningKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            this.PrivateSigningKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            File.WriteAllText(publicKeyPath, this.PublicSigningKey);
            File.WriteAllText(privateKeyPath, this.PrivateSigningKey);
        }
    }

    public static class Names
    {
        public const string LOG_PATH = nameof(Names.LOG_PATH);

        public const string CONFIG_PATH = nameof(Names.CONFIG_PATH);

        public const string PUBLIC_SIGNING_KEY = nameof(Names.PUBLIC_SIGNING_KEY);

        public const string PRIVATE_SIGNING_KEY = nameof(Names.PRIVATE_SIGNING_KEY);
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string LogPath { get; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string ConfigPath { get; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string PublicSigningKey { get; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string PrivateSigningKey { get; }
}
