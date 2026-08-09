using AeroDesk.Application.Common.Exceptions;
using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Documents.Queries.DownloadDocument
{
    public class DownloadDocumentQueryHandler
        : IRequestHandler<DownloadDocumentQuery, DownloadDocumentResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUserService _currentUserService;

        public DownloadDocumentQueryHandler(
            IApplicationDbContext context,
            IFileStorageService fileStorageService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _currentUserService = currentUserService;
        }

        public async Task<DownloadDocumentResult> Handle(
            DownloadDocumentQuery request,
            CancellationToken cancellationToken)
        {
            var document = await _context.Documents
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

            if (document == null)
            {
                throw new KeyNotFoundException($"Document with Id {request.Id} was not found.");
            }

            // Ownership check: Passenger can only download documents they uploaded themselves
            if (string.Equals(_currentUserService.Role, "Passenger", StringComparison.OrdinalIgnoreCase))
            {
                if (_currentUserService.UserId != document.UploadedByUserId)
                {
                    throw new ForbiddenAccessException("You can only download your own documents.");
                }
            }

            var stream = await _fileStorageService.GetFileAsync(document.FilePath, cancellationToken);

            return new DownloadDocumentResult
            {
                FileStream = stream,
                FileName = document.FileName,
                ContentType = document.ContentType
            };
        }
    }
}