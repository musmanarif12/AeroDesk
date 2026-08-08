using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Documents.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Documents.Queries.GetDocumentsByEntity
{
    public class GetDocumentsByEntityQueryHandler
        : IRequestHandler<GetDocumentsByEntityQuery, List<DocumentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDocumentsByEntityQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DocumentDto>> Handle(
            GetDocumentsByEntityQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Documents
                .AsNoTracking()
                .Where(d => d.EntityType.ToLower() == request.EntityType.ToLower()
                         && d.EntityId == request.EntityId
                         && !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .Select(d => new DocumentDto
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    ContentType = d.ContentType,
                    FileSizeBytes = d.FileSizeBytes,
                    EntityType = d.EntityType,
                    EntityId = d.EntityId,
                    UploadedByUserId = d.UploadedByUserId,
                    CreatedAt = d.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}