using FluentValidation;

namespace AeroDesk.Application.Features.Flights.Commands.CreateFlight
{
    public class CreateFlightCommandValidator
        : AbstractValidator<CreateFlightCommand>
    {
        public CreateFlightCommandValidator()
        {
            RuleFor(x => x.FlightNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.DepartureTime)
                .NotEmpty();

            RuleFor(x => x.ArrivalTime)
                .GreaterThan(x => x.DepartureTime);

            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.DepartureAirportId)
                .GreaterThan(0);

            RuleFor(x => x.ArrivalAirportId)
                .GreaterThan(0);

            RuleFor(x => x.GateId)
                .GreaterThan(0);

            RuleFor(x => x.AirlineId)
                .GreaterThan(0);

            RuleFor(x => x.AircraftId)
                .GreaterThan(0);
        }
    }
}