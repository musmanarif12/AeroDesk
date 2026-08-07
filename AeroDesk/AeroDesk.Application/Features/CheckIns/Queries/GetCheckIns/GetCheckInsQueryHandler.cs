using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.CheckIns.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.CheckIns.Queries.GetCheckIns
{
    public class GetCheckInsQueryHandler
        : IRequestHandler<GetCheckInsQuery, List<CheckInDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCheckInsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CheckInDto>> Handle(
            GetCheckInsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.CheckIns
                .AsNoTracking()
                .Select(x => new CheckInDto
                {
                    Id = x.Id,
                    CheckInTime = x.CheckInTime,
                    BaggageCount = x.BaggageCount,
                    Status = x.Status,
                    PassengerId = x.PassengerId,
                    BookingId = x.BookingId,
                    FlightId = x.FlightId
                })
                .ToListAsync(cancellationToken);
        }
    }
}