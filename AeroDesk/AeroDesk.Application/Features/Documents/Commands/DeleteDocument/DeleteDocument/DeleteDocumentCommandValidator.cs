using FluentValidation;

namespace AeroDesk.Application.Features.Documents.Commands.DeleteDocument
{
    public class DeleteDocumentCommandValidator : AbstractValidator<DeleteDocumentCommand>
    {
        public DeleteDocumentCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}