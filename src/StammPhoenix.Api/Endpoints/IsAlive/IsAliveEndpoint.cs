using FastEndpoints;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.IsAlive;

[PublicAPI]
public class IsAliveEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        this.Get("/is-alive");
        this.AllowAnonymous();
        this.Description(d =>
        {
            d.Produces<string>();
            d.WithTags("meta");
        });
    }

    public override async Task<string> ExecuteAsync(CancellationToken ct)
    {
        return await Task.FromResult("200 OK");
    }
}
