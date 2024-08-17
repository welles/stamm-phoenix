using FluentValidation;

namespace StammPhoenix.Application.Commands.CreateEvent;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        this.RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate);

        this.RuleFor(x => x.Link)
            .Matches("[A-Za-z0-9_]+")
            .WithMessage("Link must only contain alphanumeric characters and underlines.");
    }
}
