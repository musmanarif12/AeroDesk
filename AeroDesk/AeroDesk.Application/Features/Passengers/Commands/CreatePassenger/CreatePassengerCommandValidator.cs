using FluentValidation;

namespace AeroDesk.Application.Features.Passengers.Commands.CreatePassenger
{
    public class CreatePassengerCommandValidator
        : AbstractValidator<CreatePassengerCommand>
    {
        public CreatePassengerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Gender)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty();

            RuleFor(x => x.PassportNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Nationality)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
        }
    }
}