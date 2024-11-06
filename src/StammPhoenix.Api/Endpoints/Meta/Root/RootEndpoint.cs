using System.Reflection;
using System.Text;
using FastEndpoints;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.Meta.Root;

[PublicAPI]
public class RootEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        this.Get("/");
        this.AllowAnonymous();
        this.Summary(s =>
        {
            s.Summary = "Show metadata";
            s.Description =
                "Produces plain text containing metadata about the running instance of the API.";
        });
        this.Group<MetaGroup>();
        this.Description(d =>
        {
            d.Produces<string>();
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var versionText =
            Assembly
                .GetAssembly(typeof(Program))
                ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion.Split("+")
                .FirstOrDefault() ?? "???";

        var sb = new StringBuilder();
        sb.AppendLine("STAMM PHOENIX API");
        sb.AppendLine($"STARTUP TIME (UTC): {Program.StartupTime:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"VERSION: {versionText}");
        sb.AppendLine($"SWAGGER DOCS: {this.BaseURL}swagger");

        await this.SendStringAsync(sb.ToString(), cancellation: ct);
    }
}
