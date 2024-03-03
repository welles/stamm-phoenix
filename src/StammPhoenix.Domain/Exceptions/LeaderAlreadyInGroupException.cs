using StammPhoenix.Domain.Models;

namespace StammPhoenix.Domain.Exceptions;

public class LeaderAlreadyInGroupException(Leader leader, Group group)
    : Exception($"Leader {leader.FirstName} {leader.LastName} already exists in group {group.Name}");
