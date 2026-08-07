using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Passengers.Commands.DeletePassenger
{
    public class DeletePassengerCommandHandler
        : IRequestHandler<DeletePassengerCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeletePassengerCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeletePassengerCommand request,
            CancellationToken cancellationToken)
        {
            var passenger = await _context.Passengers
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (passenger == null)
            {
                return false;
            }

            _context.Passengers.Remove(passenger);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}