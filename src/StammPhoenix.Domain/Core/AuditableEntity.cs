namespace StammPhoenix.Domain.Core;

/// <summary>
/// An entity that tracks the last time it was changed, and who it was changed by.
/// </summary>
public abstract class AuditableEntity
{
    /// <summary>
    /// The instance this entity was last modified by.
    /// Never null, is set to the creation instance when created.
    /// </summary>
    public required string LastModifiedBy { get; set; }

    /// <summary>
    /// The time this entitiy was last modified at.
    /// Never null, is set to creation time when created.
    /// </summary>
    public required DateTime LastModifiedAt { get; set; }

    /// <summary>
    /// The instance this entitiy was created by.
    /// Never changes after creation.
    /// </summary>
    public required string CreatedBy { get; set; }

    /// <summary>
    /// The time this entitiy was created at.
    /// Never changes after creation.
    /// </summary>
    public required DateTime CreatedAt { get; set; }
}
