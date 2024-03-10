using System.ComponentModel;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.Auth.Login;

[PublicAPI]
[Description("Contains data for authorizing with the API")]
public sealed class LoginRequest
{
    [JsonPropertyName("login_email")]
    [Description("The e-mail address of the leader")]
    public string? LoginEmail { get; init; }

    [JsonPropertyName("password")]
    [Description("The password of the leader")]
    public string? Password { get; init; }
}
