using FastEndpoints;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.MetaGroup.IsAlive;

[PublicAPI]
public class IsAliveEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        this.Get("/is-alive");
        this.AllowAnonymous();
        this.Summary(s =>
        {
            s.Summary = "Check status";
            s.Description = "Returns a simple HTTP response if the API is running.";
        });
        this.Group<MetaGroup>();
        this.Description(d =>
        {
            d.Produces<string>();
        });
    }

    public override async Task<string> ExecuteAsync(CancellationToken ct)
    {
        return await Task.FromResult("200 OK");
    }
}
