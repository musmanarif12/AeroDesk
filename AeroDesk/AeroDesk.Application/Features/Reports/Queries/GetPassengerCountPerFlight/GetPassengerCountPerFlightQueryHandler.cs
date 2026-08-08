using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Reports.Queries.GetPassengerCountPerFlight
{
    public class GetPassengerCountPerFlightQueryHandler
        : IRequestHandler<GetPassengerCountPerFlightQuery, List<PassengerCountPerFlightDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetPassengerCountPerFlightQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PassengerCountPerFlightDto>> Handle(
            GetPassengerCountPerFlightQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(b => b.Status != "Cancelled")
                .GroupBy(b => new { b.FlightId, b.Flight.FlightNumber })
                .Select(g => new PassengerCountPerFlightDto
                {
                    FlightId = g.Key.FlightId,
                    FlightNumber = g.Key.FlightNumber,
                    PassengerCount = g.Count()
                })
                .OrderByDescending(x => x.PassengerCount)
                .ToListAsync(cancellationToken);
        }
    }
}