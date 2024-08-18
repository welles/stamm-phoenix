using System.Security.Claims;
using FastEndpoints.Security;
using JetBrains.Annotations;
using StammPhoenix.Api.Core;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Api.Endpoints.Auth.Login;

[PublicAPI]
public sealed class LoginEndpoint : PostEndpoint<LoginRequest, LoginResponse, AuthGroup>
{
    public IAppConfiguration AppConfiguration { get; }

    public LoginEndpoint(IAppConfiguration appConfiguration)
    {
        this.AppConfiguration = appConfiguration;
    }

    public override string EndpointRoute => "/login";

    public override string EndpointSummary => "Authorize with the API";

    public override string EndpointDescription => "Returns a JWT token on successful authorization.";

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var admin = new Leader
        {
            Id = Guid.Parse("c852035e-43c1-4f22-9e8f-1527ec825608"),
            LastName = "Adminson",
            FirstName = "Admin",
            PasswordHash = "admin",
            LoginEmail = "admin@stamm-phoenix.de",
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = "SERVER",
            LastModifiedAt = DateTimeOffset.UtcNow,
            LastModifiedBy = "SERVER"
        };

        //if (await authService.CredentialsAreValid(req.Username, req.Password, ct))
        if (req.LoginEmail == admin.LoginEmail && req.Password == admin.PasswordHash)
        {
            var jwtToken = JwtBearer.CreateToken(
                o =>
                {
                    o.SigningKey = this.AppConfiguration.PrivateSigningKey;
                    o.SigningStyle = TokenSigningStyle.Asymmetric;
                    o.KeyIsPemEncoded = true;
                    o.ExpireAt = DateTime.UtcNow.AddDays(1);
                    o.User.Roles.Add("Leader");
                    o.User.Claims.Add((ClaimTypes.Email, req.LoginEmail));
                    o.User.Claims.Add((ClaimTypes.NameIdentifier, admin.Id.ToString()));
                    o.User.Claims.Add((ClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"));
                });

            await this.SendAsync(new LoginResponse { Token = jwtToken });
        }
        else
        {
            this.ThrowError("Username or password was not correct.", StatusCodes.Status401Unauthorized);
        }
    }
}
