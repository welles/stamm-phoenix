using FastEndpoints;

namespace StammPhoenix.Api.Endpoints.Auth;

public sealed class AuthGroup : Group
{
    public const string GroupName = "Auth";

    public AuthGroup()
    {
        this.Configure("/auth", ep =>
        {
            ep.Description(d =>
            {
                d.WithTags(AuthGroup.GroupName);
            });
        });
    }
}
