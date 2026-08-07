using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.CheckIns.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.CheckIns.Queries.GetCheckInById
{
    public class GetCheckInByIdQueryHandler
        : IRequestHandler<GetCheckInByIdQuery, CheckInDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetCheckInByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CheckInDto?> Handle(
            GetCheckInByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.CheckIns
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}