using FluentValidation;

namespace AeroDesk.Application.Features.Reports.Queries.GetFlightsByAirport
{
    public class GetFlightsByAirportQueryValidator : AbstractValidator<GetFlightsByAirportQuery>
    {
        public GetFlightsByAirportQueryValidator()
        {
            RuleFor(x => x.AirportId)
                .GreaterThan(0)
                .WithMessage("AirportId must be greater than 0.");
        }
    }
}