using System.Security.Claims;
using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Api.Core;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Guid? Id
    {
        get
        {
            var guid = this.httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrWhiteSpace(guid) && Guid.TryParse(guid, out var id))
            {
                return id;
            }

            return null;
        }
    }

    public string? LoginEmail => this.httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
}
