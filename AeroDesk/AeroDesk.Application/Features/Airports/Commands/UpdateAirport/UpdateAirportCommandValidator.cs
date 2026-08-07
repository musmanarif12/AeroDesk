using FluentValidation;

namespace AeroDesk.Application.Features.Airports.Commands.UpdateAirport
{
    public class UpdateAirportCommandValidator
        : AbstractValidator<UpdateAirportCommand>
    {
        public UpdateAirportCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Valid airport id is required.");


            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Airport name is required.")
                .MaximumLength(100);


            RuleFor(x => x.IATACode)
                .NotEmpty()
                .WithMessage("IATA code is required.")
                .Length(3);


            RuleFor(x => x.ICAOCode)
                .NotEmpty()
                .WithMessage("ICAO code is required.")
                .Length(4);


            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(100);


            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required.")
                .MaximumLength(100);
        }
    }
}