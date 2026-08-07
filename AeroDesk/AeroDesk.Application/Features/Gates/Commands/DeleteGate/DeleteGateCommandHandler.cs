using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Gates.Commands.DeleteGate
{
    public class DeleteGateCommandHandler
        : IRequestHandler<DeleteGateCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteGateCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteGateCommand request,
            CancellationToken cancellationToken)
        {
            var gate = await _context.Gates
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (gate == null)
            {
                return false;
            }

            _context.Gates.Remove(gate);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}