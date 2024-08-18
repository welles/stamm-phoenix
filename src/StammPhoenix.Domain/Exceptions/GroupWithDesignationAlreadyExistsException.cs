using StammPhoenix.Domain.Enums;

namespace StammPhoenix.Domain.Exceptions;

public class GroupWithDesignationAlreadyExistsException(GroupDesignation designation) : DomainException($"Group with designation {designation} already exists");
