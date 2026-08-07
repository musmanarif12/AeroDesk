using FluentValidation;

namespace AeroDesk.Application.Features.Airports.Commands.CreateAirport
{
    public class CreateAirportCommandValidator
        : AbstractValidator<CreateAirportCommand>
    {
        public CreateAirportCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Airport name is required.")
                .MaximumLength(100)
                .WithMessage("Airport name cannot exceed 100 characters.");


            RuleFor(x => x.IATACode)
                .NotEmpty()
                .WithMessage("IATA code is required.")
                .Length(3)
                .WithMessage("IATA code must be exactly 3 characters.");


            RuleFor(x => x.ICAOCode)
                .NotEmpty()
                .WithMessage("ICAO code is required.")
                .Length(4)
                .WithMessage("ICAO code must be exactly 4 characters.");


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