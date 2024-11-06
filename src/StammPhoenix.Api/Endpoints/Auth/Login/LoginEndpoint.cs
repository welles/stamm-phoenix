using System.Security.Claims;
using FastEndpoints.Security;
using JetBrains.Annotations;
using StammPhoenix.Api.Core;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Domain.Exceptions;

namespace StammPhoenix.Api.Endpoints.Auth.Login;

[PublicAPI]
public sealed class LoginEndpoint(
    IPasswordHasher passwordHasher,
    IAppConfiguration appConfiguration,
    ILeaderRepository leaderRepository
) : PostEndpoint<LoginRequest, LoginResponse, AuthGroup>
{
    private IPasswordHasher PasswordHasher { get; } = passwordHasher;

    private IAppConfiguration AppConfiguration { get; } = appConfiguration;

    private ILeaderRepository LeaderRepository { get; } = leaderRepository;

    public override string EndpointRoute => "/login";

    public override string EndpointSummary => "Authorize with the API";

    public override string EndpointDescription =>
        "Returns a JWT token on successful authorization.";

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var leader = await this.LeaderRepository.FindLeaderByEmail(req.LoginEmail, ct);

        if (leader == null)
        {
            await Task.Delay(3000, ct);

            throw new IncorrectUsernameOrPasswordException();
        }

        if (this.PasswordHasher.VerifyPassword(req.Password, leader.PasswordHash))
        {
            var token = JwtBearer.CreateToken(o =>
            {
                o.SigningKey = this.AppConfiguration.PrivateSigningKey;
                o.SigningStyle = TokenSigningStyle.Asymmetric;
                o.KeyIsPemEncoded = true;
                o.ExpireAt = DateTime.UtcNow.AddDays(1);
                foreach (
                    var designation in leader
                        .Groups.Where(x => x.Designation != null)
                        .Select(x => x.Designation)
                )
                {
                    o.User.Roles.Add(
                        designation.ToString()
                            ?? throw new ArgumentNullException(nameof(designation))
                    );
                }
                o.User.Claims.Add((ClaimTypes.Email, leader.LoginEmail));
                o.User.Claims.Add((ClaimTypes.NameIdentifier, leader.Id.ToString()));
                o.User.Claims.Add((ClaimTypes.Name, $"{leader.FirstName} {leader.LastName}"));
            });

            await this.SendAsync(new LoginResponse { Token = token }, cancellation: ct);
        }
        else
        {
            throw new IncorrectUsernameOrPasswordException();
        }
    }
}
