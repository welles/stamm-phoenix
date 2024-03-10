using System.ComponentModel;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.Auth.Login;

[PublicAPI]
[Description("Contains the access token to the API")]
public sealed class LoginResponse
{
    [JsonPropertyName("token")]
    [Description("A JWT token containing access information")]
    public required string Token { get; init; }
}
