using AeroDesk.Application.Features.Documents.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AeroDesk.Application.Features.Documents.Commands.UpdateDocument
{
    [Authorize(Roles = "Administrator,Check-In Officer,Airline Manager")]
    public class UpdateDocumentCommand : IRequest<DocumentDto>
    {
        public int Id { get; set; }
        public IFormFile File { get; set; } = null!;
        public int UploadedByUserId { get; set; }
    }
}