using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.CheckIns.Commands.DeleteCheckIn
{
    public class DeleteCheckInCommandHandler
        : IRequestHandler<DeleteCheckInCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCheckInCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteCheckInCommand request,
            CancellationToken cancellationToken)
        {
            var checkIn = await _context.CheckIns
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (checkIn == null)
            {
                return false;
            }

            _context.CheckIns.Remove(checkIn);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}