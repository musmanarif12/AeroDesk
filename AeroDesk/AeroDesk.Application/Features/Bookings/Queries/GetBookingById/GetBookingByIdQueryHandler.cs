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

        public GetBookingByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDto?> Handle(
            GetBookingByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
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
        }
    }
}