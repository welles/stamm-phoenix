namespace StammPhoenix.Application.Interfaces;

public interface IUser
{
    public Guid? Id { get; }

    public string? LoginEmail { get; }
}
