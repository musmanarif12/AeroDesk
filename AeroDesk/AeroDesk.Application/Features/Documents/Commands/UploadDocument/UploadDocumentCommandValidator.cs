using FluentValidation;

namespace AeroDesk.Application.Features.Documents.Commands.UploadDocument
{
    public class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
    {
        private static readonly string[] AllowedExtensions =
            { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx", ".txt" };

        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

        private static readonly string[] AllowedEntityTypes =
            { "Passenger", "Booking", "CheckIn", "Airline", "Aircraft" };

        public UploadDocumentCommandValidator()
        {
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

            RuleFor(x => x.EntityType)
                .NotEmpty()
                .Must(et => AllowedEntityTypes.Contains(et))
                .WithMessage($"EntityType must be one of: {string.Join(", ", AllowedEntityTypes)}");

            RuleFor(x => x.EntityId)
                .GreaterThan(0);

            RuleFor(x => x.UploadedByUserId)
                .GreaterThan(0);
        }
    }
}