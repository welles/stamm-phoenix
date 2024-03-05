using System.Reflection;
using System.Text;
using FastEndpoints;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.Root;

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
            s.Description = "Produces plain text containing metadata about the running instance of the API.";
        });
        this.Description(d =>
        {
            d.Produces<string>();
            d.WithTags("meta");
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var versionText = Assembly.GetAssembly(typeof(Program))?
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion.Split("+")
            .FirstOrDefault() ?? "???";

        var sb = new StringBuilder();
        sb.AppendLine("STAMM PHOENIX API");
        sb.AppendLine($"STARTUP TIME (UTC): {Program.StartupTime:dd.MM.yyyy HH:mm:ss}");
        sb.AppendLine($"VERSION: {versionText}");
        sb.AppendLine($"SWAGGER DOCS: {this.BaseURL}swagger");

        await this.SendStringAsync(sb.ToString(), cancellation: ct);
    }
}
