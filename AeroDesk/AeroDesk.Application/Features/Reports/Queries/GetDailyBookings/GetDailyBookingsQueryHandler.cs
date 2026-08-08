using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Reports.Queries.GetDailyBookings
{
    public class GetDailyBookingsQueryHandler
        : IRequestHandler<GetDailyBookingsQuery, List<DailyBookingsDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDailyBookingsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DailyBookingsDto>> Handle(
            GetDailyBookingsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .GroupBy(b => b.BookingDate.Date)
                .Select(g => new DailyBookingsDto
                {
                    Date = g.Key,
                    TotalBookings = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);
        }
    }
}