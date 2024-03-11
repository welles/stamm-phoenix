using FastEndpoints;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.Auth.Login;

[PublicAPI]
public class LoginOptionsEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        this.Routes("/login");
        this.Verbs("OPTIONS");
        this.Group<AuthGroup>();
        this.AllowAnonymous(["OPTIONS"]);
        this.Description(b =>
        {
            b.ExcludeFromDescription();
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await this.SendHeadersAsync(x => x.Append("Allow", Http.POST.ToString()), cancellation: ct);
    }
}
