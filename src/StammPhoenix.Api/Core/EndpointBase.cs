using FastEndpoints;
using Microsoft.IdentityModel.Tokens;

namespace StammPhoenix.Api.Core;

public abstract class EndpointBase<TRequest, TResponse, TGroup> : Endpoint<TRequest, TResponse>
    where TRequest : notnull
    where TGroup : EndpointGroup, new()
{
    public virtual string[] EndpointRoles { get; } = [];

    public abstract string EndpointRoute { get; }

    public abstract string EndpointSummary { get; }

    public abstract string EndpointDescription { get; }

    public abstract Http[] EndpointVerbs { get; }

    public override void Configure()
    {
        this.Verbs(this.EndpointVerbs);

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
