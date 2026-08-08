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

        public DownloadDocumentQueryHandler(
            IApplicationDbContext context,
            IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
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