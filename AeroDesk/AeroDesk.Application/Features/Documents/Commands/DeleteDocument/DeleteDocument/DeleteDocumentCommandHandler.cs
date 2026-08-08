using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Documents.Commands.DeleteDocument
{
    public class DeleteDocumentCommandHandler
        : IRequestHandler<DeleteDocumentCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDocumentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

            if (document == null)
            {
                throw new KeyNotFoundException($"Document with Id {request.Id} was not found.");
            }

            document.IsDeleted = true;
            document.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}