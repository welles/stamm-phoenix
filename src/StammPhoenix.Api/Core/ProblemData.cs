using System.Collections;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Core;

[PublicAPI]
public record ProblemData
{
    public required string Type { get; init; }

    public required int Code { get; init; }

    public required string Message { get; init; }

    public IDictionary? Data { get; init; }
}
