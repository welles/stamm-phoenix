namespace StammPhoenix.Domain.Exceptions;

public class StatusCodeException(int statusCode, string message) : DomainException(message)
{
    public int StatusCode { get; } = statusCode;
}
