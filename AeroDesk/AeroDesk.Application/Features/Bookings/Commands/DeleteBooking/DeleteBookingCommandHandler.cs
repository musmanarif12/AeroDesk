using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandHandler
        : IRequestHandler<DeleteBookingCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBookingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteBookingCommand request,
            CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}