using AeroDesk.Application.Features.Documents.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AeroDesk.Application.Features.Documents.Commands.UploadDocument
{
    public class UploadDocumentCommand : IRequest<DocumentDto>
    {
        public IFormFile File { get; set; } = null!;
        public string EntityType { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public int UploadedByUserId { get; set; }
    }
}