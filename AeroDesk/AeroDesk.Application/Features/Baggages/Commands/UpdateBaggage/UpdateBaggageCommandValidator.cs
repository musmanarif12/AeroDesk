using FluentValidation;

namespace AeroDesk.Application.Features.Baggages.Commands.UpdateBaggage
{
    public class UpdateBaggageCommandValidator
        : AbstractValidator<UpdateBaggageCommand>
    {
        public UpdateBaggageCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Valid baggage id is required.");

            RuleFor(x => x.Weight)
                .GreaterThan(0)
                .WithMessage("Weight must be greater than zero.");

            RuleFor(x => x.TagNumber)
                .NotEmpty()
                .WithMessage("Tag number is required.")
                .MaximumLength(50);

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required.")
                .MaximumLength(50);

            RuleFor(x => x.PassengerId)
                .GreaterThan(0)
                .WithMessage("Valid Passenger is required.");
        }
    }
}