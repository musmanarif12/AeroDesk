using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Reports.Queries.GetAvailableSeats
{
    public class GetAvailableSeatsQueryHandler
        : IRequestHandler<GetAvailableSeatsQuery, List<AvailableSeatsDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAvailableSeatsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AvailableSeatsDto>> Handle(
            GetAvailableSeatsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Flights
                .AsNoTracking()
                .Select(f => new AvailableSeatsDto
                {
                    FlightId = f.Id,
                    FlightNumber = f.FlightNumber,
                    Capacity = f.Aircraft.Capacity,
                    BookedSeats = f.Bookings.Count(b => b.Status != "Cancelled"),
                    AvailableSeats = f.Aircraft.Capacity
                        - f.Bookings.Count(b => b.Status != "Cancelled")
                })
                .OrderBy(x => x.FlightNumber)
                .ToListAsync(cancellationToken);
        }
    }
}