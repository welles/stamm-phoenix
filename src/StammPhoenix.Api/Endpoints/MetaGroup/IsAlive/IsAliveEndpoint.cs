using System.Reflection;
using FastEndpoints;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.MetaGroup.IsAlive;

[PublicAPI]
public class IsAliveEndpoint : EndpointWithoutRequest<IsAliveResponse>
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
            d.Produces<IsAliveResponse>();
        });
    }

    public override async Task<IsAliveResponse> ExecuteAsync(CancellationToken ct)
    {
        var versionText = Assembly.GetAssembly(typeof(Program))?
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion.Split("+")
            .FirstOrDefault() ?? "???";

        var startupTime = Program.StartupTime.ToString("O");

        return await Task.FromResult(new IsAliveResponse
        {
            Version = versionText,
            Status = "OK",
            StartupTime = startupTime
        });
    }
}
