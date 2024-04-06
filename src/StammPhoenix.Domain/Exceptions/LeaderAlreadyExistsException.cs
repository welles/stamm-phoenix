namespace StammPhoenix.Domain.Exceptions;

public class LeaderAlreadyExistsException(string loginEmail) : Exception($"Leader with login e-mail {loginEmail} already exists");
