using FluentValidation;

namespace AeroDesk.Application.Features.Passengers.Commands.UpdatePassenger
{
    public class UpdatePassengerCommandValidator
        : AbstractValidator<UpdatePassengerCommand>
    {
        public UpdatePassengerCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

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