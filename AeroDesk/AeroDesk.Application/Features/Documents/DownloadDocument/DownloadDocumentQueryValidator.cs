using FluentValidation;

namespace AeroDesk.Application.Features.Documents.Queries.DownloadDocument
{
    public class DownloadDocumentQueryValidator : AbstractValidator<DownloadDocumentQuery>
    {
        public DownloadDocumentQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}