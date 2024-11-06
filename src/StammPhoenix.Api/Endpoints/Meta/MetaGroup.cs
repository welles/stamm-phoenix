using JetBrains.Annotations;
using StammPhoenix.Api.Core;

namespace StammPhoenix.Api.Endpoints.Meta;

[PublicAPI]
public class MetaGroup()
    : EndpointGroup("Meta", string.Empty, "Endpoints concerning metadata about the API");
