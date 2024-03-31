namespace StammPhoenix.Application.Interfaces;

public interface IDatabaseConfiguration
{
    public string Host { get; }

    public int Port { get; }

    public string Database { get; }

    public string User { get; }

    public string Password { get; }
}
