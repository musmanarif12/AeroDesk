using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Reports.Common;
using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Reports.Queries.GetDashboardSummary
{
    public class GetDashboardSummaryQueryHandler
        : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDto>
    {
        private readonly IApplicationDbContext _context;

        private static readonly string[] ActiveStatuses =
            { "Scheduled", "Boarding", "In Air" };

        public GetDashboardSummaryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> Handle(
            GetDashboardSummaryQuery request,
            CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date;

            var flightsToday = await _context.Flights
                .AsNoTracking()
                .CountAsync(f => f.DepartureTime.Date == today, cancellationToken);

            var activeFlights = await _context.Flights
                .AsNoTracking()
                .CountAsync(f => ActiveStatuses.Contains(f.Status), cancellationToken);

            var totalPassengers = await _context.Passengers
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var delayedFlights = await _context.Flights
                .AsNoTracking()
                .CountAsync(f => f.Status == "Delayed", cancellationToken);

            var todayBookings = await _context.Bookings
                .AsNoTracking()
                .CountAsync(b => b.BookingDate.Date == today, cancellationToken);

            // Revenue: fetch non-cancelled bookings' TravelClass, then price in-memory
            var travelClasses = await _context.Bookings
                .AsNoTracking()
                .Where(b => b.Status != "Cancelled")
                .Select(b => b.TravelClass)
                .ToListAsync(cancellationToken);

            var totalRevenue = travelClasses
                .Sum(tc => TravelClassPricing.GetPrice(tc));

            return new DashboardSummaryDto
            {
                FlightsToday = flightsToday,
                ActiveFlights = activeFlights,
                TotalPassengers = totalPassengers,
                DelayedFlights = delayedFlights,
                TodayBookings = todayBookings,
                TotalRevenue = totalRevenue
            };
        }
    }
}