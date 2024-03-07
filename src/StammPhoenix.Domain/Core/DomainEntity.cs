using MediatR;

namespace StammPhoenix.Domain.Core;

/// <summary>
/// Entity that supports domain event propagation.
/// </summary>
public abstract class DomainEntity : IdentifiableEntity
{
    private readonly List<INotification> _domainEvents = new();

    /// <summary>
    /// The domain events of this entity.
    /// </summary>
    public IReadOnlyCollection<INotification> DomainEvents => this._domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a new domain event concerning this entity.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    public void AddDomainEvent(INotification domainEvent)
    {
        this._domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Removes domain event from this entity.
    /// </summary>
    /// <param name="domainEvent">The domain event to remove.</param>
    public void RemoveDomainEvent(INotification domainEvent)
    {
        this._domainEvents.Remove(domainEvent);
    }

    /// <summary>
    /// Clears all domain events from this entity.
    /// </summary>
    public void ClearDomainEvents()
    {
        this._domainEvents.Clear();
    }
}
