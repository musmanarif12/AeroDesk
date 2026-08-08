using MediatR;

namespace AeroDesk.Application.Features.Documents.Queries.DownloadDocument
{
    public class DownloadDocumentQuery : IRequest<DownloadDocumentResult>
    {
        public int Id { get; set; }
    }
}