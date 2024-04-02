using CommandLine;
using JetBrains.Annotations;

namespace StammPhoenix.Cli.Options;

[PublicAPI]
public abstract class DatabaseOptionsBase
{
    [Option("host", Required = true)]
    public required string Host { get; set; }

    [Option("port", Required = true)]
    public required int Port { get; set; }

    [Option("database", Required = true)]
    public required string Database { get; set; }

    [Option("user", Required = true)]
    public required string User { get; set; }

    [Option("password", Required = true)]
    public required string Password { get; set; }
}
