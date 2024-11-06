using System.Security.Claims;
using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Api.Core;

public class ApiCurrentUser : ICurrentUser
{
    private IHttpContextAccessor HttpContextAccessor { get; }

    public ApiCurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        this.HttpContextAccessor = httpContextAccessor;
    }

    public string Name =>
        this.HttpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "Anonymous";
}
