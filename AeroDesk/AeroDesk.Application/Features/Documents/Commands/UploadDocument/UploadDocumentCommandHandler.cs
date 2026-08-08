using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Documents.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.Documents.Commands.UploadDocument
{
    public class UploadDocumentCommandHandler
        : IRequestHandler<UploadDocumentCommand, DocumentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public UploadDocumentCommandHandler(
            IApplicationDbContext context,
            IFileStorageService fileStorageService,
            IMapper mapper)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        public async Task<DocumentDto> Handle(
            UploadDocumentCommand request,
            CancellationToken cancellationToken)
        {
            await using var stream = request.File.OpenReadStream();

            var (storedFileName, filePath) = await _fileStorageService.SaveFileAsync(
                stream,
                request.File.FileName,
                cancellationToken);

            var document = new Document
            {
                FileName = request.File.FileName,
                StoredFileName = storedFileName,
                FilePath = filePath,
                ContentType = request.File.ContentType,
                FileSizeBytes = request.File.Length,
                EntityType = request.EntityType,
                EntityId = request.EntityId,
                UploadedByUserId = request.UploadedByUserId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DocumentDto>(document);
        }
    }
}