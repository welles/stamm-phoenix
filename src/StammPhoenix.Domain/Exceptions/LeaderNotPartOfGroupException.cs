using StammPhoenix.Domain.Models;

namespace StammPhoenix.Domain.Exceptions;

public class LeaderNotPartOfGroupException(Leader leader, Group group)
    : DomainException($"Leader {leader.FirstName} {leader.LastName} is not part of group {group.Name}");
