namespace StammPhoenix.Domain.Exceptions;

public class EventLinkAlreadyExistsException(string link) : DomainException($"Event with link {link} already exists.");
