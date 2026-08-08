using FluentValidation;

namespace AeroDesk.Application.Features.Reports.Queries.GetFlightsByAirline
{
    public class GetFlightsByAirlineQueryValidator : AbstractValidator<GetFlightsByAirlineQuery>
    {
        public GetFlightsByAirlineQueryValidator()
        {
            RuleFor(x => x.AirlineId)
                .GreaterThan(0)
                .WithMessage("AirlineId must be greater than 0.");
        }
    }
}