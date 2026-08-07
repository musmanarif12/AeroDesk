using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Bookings.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Bookings.Queries.GetBookings
{
    public class GetBookingsQueryHandler
        : IRequestHandler<GetBookingsQuery, List<BookingDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetBookingsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingDto>> Handle(
            GetBookingsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
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
                .ToListAsync(cancellationToken);
        }
    }
}