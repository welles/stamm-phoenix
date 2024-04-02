using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Cli.Core;

public class CliUser : IUser
{
    public Guid? Id => Guid.Empty;

    public string? LoginEmail => "CLI";
}
