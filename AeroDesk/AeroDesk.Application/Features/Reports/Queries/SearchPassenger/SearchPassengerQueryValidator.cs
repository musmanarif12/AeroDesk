using FluentValidation;

namespace AeroDesk.Application.Features.Reports.Queries.SearchPassenger
{
    public class SearchPassengerQueryValidator : AbstractValidator<SearchPassengerQuery>
    {
        public SearchPassengerQueryValidator()
        {
            RuleFor(x => x.SearchTerm)
                .NotEmpty()
                .WithMessage("Search term is required.")
                .MinimumLength(2)
                .WithMessage("Search term must be at least 2 characters.");
        }
    }
}