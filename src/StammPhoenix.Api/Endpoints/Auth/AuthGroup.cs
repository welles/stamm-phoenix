using JetBrains.Annotations;
using StammPhoenix.Api.Core;

namespace StammPhoenix.Api.Endpoints.Auth;

[PublicAPI]
public class AuthGroup() : EndpointGroup("Auth", "/auth", "Endpoints for authorizing with the API");
