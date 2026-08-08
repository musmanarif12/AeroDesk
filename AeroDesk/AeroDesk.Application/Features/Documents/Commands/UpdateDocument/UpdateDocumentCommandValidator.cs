using FluentValidation;

namespace AeroDesk.Application.Features.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentCommandValidator : AbstractValidator<UpdateDocumentCommand>
    {
        private static readonly string[] AllowedExtensions =
            { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx", ".txt" };

        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

        public UpdateDocumentCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("File is required.");

            RuleFor(x => x.File)
                .Must(f => f.Length > 0)
                .WithMessage("File cannot be empty.")
                .Must(f => f.Length <= MaxFileSizeBytes)
                .WithMessage("File size must not exceed 5 MB.")
                .Must(f => AllowedExtensions.Contains(
                    Path.GetExtension(f.FileName).ToLowerInvariant()))
                .WithMessage($"File type not allowed. Allowed types: {string.Join(", ", AllowedExtensions)}")
                .When(x => x.File != null);

            RuleFor(x => x.UploadedByUserId).GreaterThan(0);
        }
    }
}