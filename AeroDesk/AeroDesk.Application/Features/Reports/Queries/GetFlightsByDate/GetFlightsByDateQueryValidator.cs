using FluentValidation;

namespace AeroDesk.Application.Features.Reports.Queries.GetFlightsByDate
{
    public class GetFlightsByDateQueryValidator : AbstractValidator<GetFlightsByDateQuery>
    {
        public GetFlightsByDateQueryValidator()
        {
            RuleFor(x => x.Date)
                .NotEqual(default(DateTime))
                .WithMessage("Date is required.");
        }
    }
}