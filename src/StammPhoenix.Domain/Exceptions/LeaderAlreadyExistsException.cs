namespace StammPhoenix.Domain.Exceptions;

public class LeaderAlreadyExistsException(string loginEmail) : DomainException($"Leader with login e-mail {loginEmail} already exists");
