using FluentValidation;

namespace AeroDesk.Application.Features.Aircrafts.Commands.CreateAircraft
{
    public class CreateAircraftCommandValidator
        : AbstractValidator<CreateAircraftCommand>
    {
        public CreateAircraftCommandValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);


            RuleFor(x => x.Model)
                .NotEmpty()
                .MaximumLength(100);


            RuleFor(x => x.Manufacturer)
                .NotEmpty()
                .MaximumLength(100);


            RuleFor(x => x.Capacity)
                .GreaterThan(0);


            RuleFor(x => x.RegistrationNumber)
                .NotEmpty()
                .MaximumLength(50);


            RuleFor(x => x.AirlineId)
                .GreaterThan(0);
        }
    }
}