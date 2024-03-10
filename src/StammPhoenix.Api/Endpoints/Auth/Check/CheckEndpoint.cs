using FastEndpoints;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.Auth.Check;

[PublicAPI]
public sealed class CheckEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        this.Get("/check");
        this.Roles("Leader");
        this.Group<AuthGroup>();
        this.Summary(s =>
        {
            s.Summary = "Check if the JWT token is valid";
            s.Description = "Checks if the given bearer token is valid. Returns a 204 No Content response if validation was successful.";
        });
        this.Description(d =>
        {
            d.Produces(StatusCodes.Status204NoContent);
            d.Produces(StatusCodes.Status401Unauthorized);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await this.SendNoContentAsync(ct);
    }
}
