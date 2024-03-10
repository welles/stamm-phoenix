using System.Security.Claims;
using System.Security.Cryptography;
using FastEndpoints;
using FastEndpoints.Security;
using JetBrains.Annotations;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Models;

namespace StammPhoenix.Api.Endpoints.Auth.Login;

[PublicAPI]
public sealed class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    public IAppConfiguration AppConfiguration { get; }

    public LoginEndpoint(IAppConfiguration appConfiguration)
    {
        this.AppConfiguration = appConfiguration;
    }

    public override void Configure()
    {
        this.Post("/login");
        this.Group<AuthGroup>();
        this.AllowAnonymous();
        this.Summary(s =>
        {
            s.Summary = "Authorize with the API";
            s.Description = "Returns a JWT token on successful authorization.";
        });
        this.Description(d =>
        {
            d.Produces(StatusCodes.Status401Unauthorized);
        });
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var admin = new Leader
        {
            Id = Guid.Parse("c852035e-43c1-4f22-9e8f-1527ec825608"),
            LastName = "Adminson",
            FirstName = "Admin",
            PasswordHash = "admin",
            LoginEmail = "admin@stamm-phoenix.de",
            CreatedAt = DateTimeOffset.Now,
            CreatedBy = "SERVER",
            LastModifiedAt = DateTimeOffset.Now,
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
