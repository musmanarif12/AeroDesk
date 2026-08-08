using MediatR;

namespace AeroDesk.Application.Features.Documents.Commands.DeleteDocument
{
    public class DeleteDocumentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}