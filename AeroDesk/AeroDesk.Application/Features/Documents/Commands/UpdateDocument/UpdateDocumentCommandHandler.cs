using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Documents.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentCommandHandler
        : IRequestHandler<UpdateDocumentCommand, DocumentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public UpdateDocumentCommandHandler(
            IApplicationDbContext context,
            IFileStorageService fileStorageService,
            IMapper mapper)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        public async Task<DocumentDto> Handle(
            UpdateDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var oldDocument = await _context.Documents
                .FirstOrDefaultAsync(d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

            if (oldDocument == null)
            {
                throw new KeyNotFoundException($"Document with Id {request.Id} was not found.");
            }

            // Step 1: Soft delete old document version
            oldDocument.IsDeleted = true;
            oldDocument.UpdatedAt = DateTime.UtcNow;

            // Step 2: Save new file to storage
            await using var stream = request.File.OpenReadStream();
            var (storedFileName, filePath) = await _fileStorageService.SaveFileAsync(
                stream,
                request.File.FileName,
                cancellationToken);

            // Step 3: Create new active document version
            var newDocument = new Document
            {
                FileName = request.File.FileName,
                StoredFileName = storedFileName,
                FilePath = filePath,
                ContentType = request.File.ContentType,
                FileSizeBytes = request.File.Length,
                EntityType = oldDocument.EntityType,
                EntityId = oldDocument.EntityId,
                UploadedByUserId = request.UploadedByUserId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Documents.Add(newDocument);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DocumentDto>(newDocument);
        }
    }
}