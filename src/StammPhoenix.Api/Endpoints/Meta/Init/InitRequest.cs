namespace StammPhoenix.Api.Endpoints.Meta.Init;

public record InitRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    string? Address
);
