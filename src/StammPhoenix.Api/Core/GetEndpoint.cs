using FastEndpoints;
using Microsoft.IdentityModel.Tokens;

namespace StammPhoenix.Api.Core;

public abstract class GetEndpoint<TRequest, TResponse, TGroup> : Endpoint<TRequest, TResponse>
    where TRequest : notnull
    where TGroup : EndpointGroup, new()
{
    public abstract string EndpointRoute { get; }

    public abstract string EndpointSummary { get; }

    public abstract string EndpointDescription { get; }

    public virtual string[] EndpointRoles { get; } = Array.Empty<string>();

    public override void Configure()
    {
        this.Verbs(Http.GET);

        this.Routes(this.EndpointRoute);

        this.Group<TGroup>();

        this.Summary(s =>
        {
            s.Summary = this.EndpointSummary;
            s.Description = this.EndpointDescription;
        });

        if (this.EndpointRoles.IsNullOrEmpty())
        {
            this.AllowAnonymous();
        }
        else
        {
            this.Roles(this.EndpointRoles);
        }
    }
}

public abstract class GetEndpoint<TRequest, TGroup> : GetEndpoint<TRequest, object?, TGroup>
    where TRequest : notnull
    where TGroup : EndpointGroup, new();

public abstract class GetEndpointWithoutRequest<TResponse, TGroup> : GetEndpoint<EmptyRequest, TResponse, TGroup>
    where TGroup : EndpointGroup, new();

public abstract class GetEndpointWithoutRequest<TGroup> : GetEndpoint<EmptyRequest, object?, TGroup>
    where TGroup : EndpointGroup, new();
