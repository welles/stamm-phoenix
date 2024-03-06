using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StammPhoenix.Api.Endpoints.MetaGroup.IsAlive;

[Description("Contains runtime metadata about the API instance.")]
public sealed class IsAliveResponse
{
    [JsonPropertyName("status")]
    [Description("Contains the current running status of the API.")]
    public required string Status { get; init; }

    [JsonPropertyName("startup_time")]
    [Description("The startup time of the API in ISO 8601 format.")]
    public required string StartupTime { get; init; }

    [JsonProperty("version")]
    [Description("The version of the API.")]
    public required string Version { get; init; }
}
