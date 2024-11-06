namespace StammPhoenix.Domain.Exceptions;

public class EventNotFoundException(Guid id)
    : DomainException($"Event with the ID {id} does not exist");
