using AeroDesk.Application.Features.Documents.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Documents.Queries.GetDocumentsByEntity
{
    public class GetDocumentsByEntityQuery : IRequest<List<DocumentDto>>
    {
        public string EntityType { get; set; } = string.Empty;
        public int EntityId { get; set; }
    }
}