using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.Auth.Login;

[PublicAPI]
public class LoginRequestValidator : Validator<LoginRequest>
{
    public LoginRequestValidator()
    {
        this.RuleFor(x => x.LoginEmail)
            .NotEmpty()
            .WithMessage("E-Mail-Adresse darf nicht leer sein.")
            .EmailAddress()
            .WithMessage("E-Mail-Adresse muss eine gültige E-Mail-Adresse sein.");

        this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password darf nicht leer sein.");
    }
}
