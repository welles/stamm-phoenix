using FastEndpoints;
using Microsoft.IdentityModel.Tokens;
using StammPhoenix.Domain.Enums;

namespace StammPhoenix.Api.Core;

public abstract class EndpointBase<TRequest, TResponse, TGroup> : Endpoint<TRequest, TResponse>
    where TRequest : notnull
    where TGroup : EndpointGroup, new()
{
    public virtual GroupDesignation[] EndpointRoles { get; } = [];

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
            var roles = this.EndpointRoles.ToList();

            if (!roles.Contains(GroupDesignation.Admins))
            {
                roles.Add(GroupDesignation.Admins);
            }

            this.Roles(roles.Select(x => x.ToString()).ToArray());
        }
    }
}
