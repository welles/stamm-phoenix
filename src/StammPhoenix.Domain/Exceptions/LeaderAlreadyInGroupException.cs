using StammPhoenix.Domain.Models;

namespace StammPhoenix.Domain.Exceptions;

public class LeaderAlreadyInGroupException(Leader leader, Group group)
    : DomainException(
        $"Leader {leader.FirstName} {leader.LastName} already exists in group {group.Name}"
    );
