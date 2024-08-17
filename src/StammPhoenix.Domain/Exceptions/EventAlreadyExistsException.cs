namespace StammPhoenix.Domain.Exceptions;

public class EventAlreadyExistsException(string title, DateOnly startDate) : DomainException($"An event called {title} already exists for the year {startDate.Year}.");
