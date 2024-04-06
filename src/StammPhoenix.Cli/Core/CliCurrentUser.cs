using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Cli.Core;

public class CliCurrentUser : ICurrentUser
{
    public string Name => Environment.UserName;
}
