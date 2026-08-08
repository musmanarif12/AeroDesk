using AeroDesk.Application.Features.Documents.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AeroDesk.Application.Features.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentCommand : IRequest<DocumentDto>
    {
        public int Id { get; set; }
        public IFormFile File { get; set; } = null!;
        public int UploadedByUserId { get; set; }
    }
}