using System.Security.Cryptography;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Infrastructure.Configuration;

namespace StammPhoenix.Api.Core;

public class EnvironmentAppConfiguration : IAppConfiguration
{
    public EnvironmentAppConfiguration()
    {
        var logPath = Environment.GetEnvironmentVariable(EnvironmentNames.LOG_PATH);

        if (string.IsNullOrWhiteSpace(logPath))
        {
            throw new ArgumentNullException(
                EnvironmentNames.LOG_PATH,
                "Environment variable must be set."
            );
        }

        var configPath = Environment.GetEnvironmentVariable(EnvironmentNames.CONFIG_PATH);

        if (string.IsNullOrWhiteSpace(configPath))
        {
            throw new ArgumentNullException(
                EnvironmentNames.CONFIG_PATH,
                "Environment variable must be set."
            );
        }

        this.LogPath = logPath;
        this.ConfigPath = configPath;

        var publicKeyPath = Path.Combine(configPath, "JWT.public.pem");
        var privateKeyPath = Path.Combine(configPath, "JWT.private.pem");
        if (File.Exists(publicKeyPath) && File.Exists(privateKeyPath))
        {
            this.PublicSigningKey = File.ReadAllText(publicKeyPath);
            this.PrivateSigningKey = File.ReadAllText(privateKeyPath);
        }
        else
        {
            var rsa = RSA.Create(2048);
            this.PublicSigningKey = rsa.ExportRSAPublicKeyPem();
            this.PrivateSigningKey = rsa.ExportRSAPrivateKeyPem();

            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            File.WriteAllText(publicKeyPath, this.PublicSigningKey);
            File.WriteAllText(privateKeyPath, this.PrivateSigningKey);
        }

        var allowedHosts = Environment.GetEnvironmentVariable(EnvironmentNames.ALLOWED_HOSTS);

        if (string.IsNullOrWhiteSpace(allowedHosts))
        {
            throw new ArgumentNullException(
                EnvironmentNames.ALLOWED_HOSTS,
                "Environment variable must be set."
            );
        }

        this.AllowedHosts = allowedHosts.Split(';');
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
    public string[] AllowedHosts { get; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string PublicSigningKey { get; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string PrivateSigningKey { get; }
}
