using FastEndpoints;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Api.Core;

namespace StammPhoenix.Api.Endpoints.Meta.Init;

[PublicAPI]
public class InitEndpoint(IMediator mediator, IMapper mapper) : PostEndpoint<InitRequest, MetaGroup>
{
    private IMediator Mediator { get; } = mediator;

    private IMapper Mapper { get; } = mapper;

    public override string EndpointRoute => "/init";

    public override string EndpointSummary => string.Empty;

    public override string EndpointDescription => string.Empty;

    public override void Configure()
    {
        base.Configure();

        this.Description(b =>
        {
            b.ExcludeFromDescription();
        });
    }

    public override async Task HandleAsync(InitRequest req, CancellationToken ct)
    {
        var command = this.Mapper.InitRequestToSeedDatabaseCommand(req);

        await this.Mediator.Send(command, ct);
    }
}
