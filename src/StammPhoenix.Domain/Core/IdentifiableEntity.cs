namespace StammPhoenix.Domain.Core;

/// <summary>
/// An entity that is uniquely identifiable by an unchanging GUID.
/// </summary>
public abstract class IdentifiableEntity : AuditableEntity
{
    /// <summary>
    /// The globally unique ID of the entity.
    /// </summary>
    public required Guid Id { get; init; }
}
