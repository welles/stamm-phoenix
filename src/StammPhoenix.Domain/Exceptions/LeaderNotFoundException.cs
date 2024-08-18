namespace StammPhoenix.Domain.Exceptions;

public class LeaderNotFoundException(Guid leaderId) : DomainException($"Leader with ID {leaderId} not found");
