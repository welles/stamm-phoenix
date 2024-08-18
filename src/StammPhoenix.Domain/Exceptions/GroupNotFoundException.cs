namespace StammPhoenix.Domain.Exceptions;

public class GroupNotFoundException(Guid groupId) : DomainException($"Group with ID {groupId} not found");
