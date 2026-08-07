using FluentValidation;

namespace AeroDesk.Application.Features.Airlines.Commands.CreateAirline
{
    public class CreateAirlineCommandValidator
        : AbstractValidator<CreateAirlineCommand>
    {
        public CreateAirlineCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Code)
                .NotEmpty()
                .Length(2, 5);

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
        }
    }
}