using FluentValidation;

namespace StammPhoenix.Application.Commands.CreateEvent;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        this.RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Title is required");

        this.RuleFor(x => x.StartDate).NotNull().NotEmpty().WithMessage("Start date is required");

        this.RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);

        this.RuleFor(x => x.Link)
            .NotNull()
            .NotEmpty()
            .Matches("[A-Za-z0-9_]+")
            .WithMessage("Link must only contain alphanumeric characters and underlines");
    }
}
