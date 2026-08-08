using FluentValidation;

namespace AeroDesk.Application.Features.Documents.Queries.GetDocumentsByEntity
{
    public class GetDocumentsByEntityQueryValidator : AbstractValidator<GetDocumentsByEntityQuery>
    {
        public GetDocumentsByEntityQueryValidator()
        {
            RuleFor(x => x.EntityType).NotEmpty();
            RuleFor(x => x.EntityId).GreaterThan(0);
        }
    }
}