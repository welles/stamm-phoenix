using JetBrains.Annotations;
using StammPhoenix.Api.Core;

namespace StammPhoenix.Api.Endpoints.Events;

[PublicAPI]
public class EventsGroup() : EndpointGroup("Events", "/events", "Endpoints for handling events");
