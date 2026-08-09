using AeroDesk.Application.Common.Exceptions;
using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Bookings.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Bookings.Queries.GetBookingById
{
    public class GetBookingByIdQueryHandler
        : IRequestHandler<GetBookingByIdQuery, BookingDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetBookingByIdQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<BookingDto?> Handle(
            GetBookingByIdQuery request,
            CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new BookingDto
                {
                    Id = x.Id,
                    BookingReference = x.BookingReference,
                    BookingDate = x.BookingDate,
                    SeatNumber = x.SeatNumber,
                    TravelClass = x.TravelClass,
                    Status = x.Status,
                    PassengerId = x.PassengerId,
                    FlightId = x.FlightId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (booking == null)
            {
                return null;
            }

            // Ownership check: Passenger can only view their own booking
            if (string.Equals(_currentUserService.Role, "Passenger", StringComparison.OrdinalIgnoreCase))
            {
                if (_currentUserService.PassengerId != booking.PassengerId)
                {
                    throw new ForbiddenAccessException("You can only view your own booking.");
                }
            }

            return booking;
        }
    }
}