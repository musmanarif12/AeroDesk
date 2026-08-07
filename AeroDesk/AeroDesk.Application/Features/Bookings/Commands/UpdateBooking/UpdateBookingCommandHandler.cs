using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Bookings.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler
        : IRequestHandler<UpdateBookingCommand, BookingDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookingCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookingDto?> Handle(
            UpdateBookingCommand request,
            CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (booking == null)
            {
                return null;
            }

            _mapper.Map(request, booking);

            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}