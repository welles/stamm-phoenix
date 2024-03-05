namespace StammPhoenix.Domain.Enums;

/// <summary>
/// A special designation marking a group as one of a certain type.
/// Every designation can only have one group attached to it.
/// </summary>
public enum GroupDesignation
{
    Woelflinge     = 0,
    Jungpfadfinder = 1,
    Pfadfinder     = 2,
    Rover          = 3,
    Leitende       = 4,
    Vorstand       = 5
}
