using FastEndpoints;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Core;

[PublicAPI]
public abstract class EndpointGroup : Group
{
    public string GroupName { get; }

    public string RoutePrefix { get; }

    public string Description { get; }

    public EndpointGroup(string groupName, string routePrefix, string description)
    {
        this.GroupName = groupName;
        this.RoutePrefix = routePrefix;
        this.Description = description;

        this.Configure(
            routePrefix,
            ep =>
            {
                ep.Description(d =>
                {
                    d.WithTags(groupName);
                });
            }
        );
    }
}
