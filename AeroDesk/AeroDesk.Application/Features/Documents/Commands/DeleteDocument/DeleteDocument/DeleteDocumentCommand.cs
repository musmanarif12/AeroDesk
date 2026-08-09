using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Documents.Commands.DeleteDocument
{
    [Authorize(Roles = "Administrator,Check-In Officer,Airline Manager")]
    public class DeleteDocumentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}