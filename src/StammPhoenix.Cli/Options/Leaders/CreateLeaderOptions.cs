using CommandLine;
using JetBrains.Annotations;

namespace StammPhoenix.Cli.Options.Leaders;

[PublicAPI]
[Verb("create")]
public class CreateLeaderOptions : LeaderOptionsBase
{
    [Option("login-email", Required = true)]
    public required string LoginEmail { get; set; }

    [Option("login-password", Required = true)]
    public required string LoginPassword { get; set; }

    [Option("first-name", Required = true)]
    public required string FirstName { get; set; }

    [Option("last-name", Required = true)]
    public required string LastName { get; set; }

    [Option("phone-number")]
    public string? PhoneNumber { get; set; }

    [Option("address")]
    public string? Address { get; set; }
}
