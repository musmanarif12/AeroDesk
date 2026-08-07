using FluentValidation;

namespace AeroDesk.Application.Features.Airlines.Commands.UpdateAirline
{
    public class UpdateAirlineCommandValidator
        : AbstractValidator<UpdateAirlineCommand>
    {
        public UpdateAirlineCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Valid airline id is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Airline name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Airline code is required.")
                .Length(2, 5);

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required.")
                .MaximumLength(100);

            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .WithMessage("Contact number is required.")
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress();
        }
    }
}