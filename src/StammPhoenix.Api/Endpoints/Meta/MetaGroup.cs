using FastEndpoints;

namespace StammPhoenix.Api.Endpoints.Meta;

public sealed class MetaGroup : Group
{
    public const string GroupName = "Meta";

    public MetaGroup()
    {
        this.Configure(string.Empty, ep =>
        {
            ep.Description(d =>
            {
                d.WithTags(MetaGroup.GroupName);
            });
        });
    }
}
