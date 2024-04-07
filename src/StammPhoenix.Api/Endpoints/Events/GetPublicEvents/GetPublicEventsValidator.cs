using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;

namespace StammPhoenix.Api.Endpoints.Events.GetPublicEvents;

[PublicAPI]
public class GetPublicEventsValidator : Validator<GetPublicEventsRequest>
{
    public GetPublicEventsValidator()
    {
        this.RuleFor(x => x.Year)
            .NotEmpty()
            .WithMessage("Das Jahr muss angegeben werden");
    }
}
