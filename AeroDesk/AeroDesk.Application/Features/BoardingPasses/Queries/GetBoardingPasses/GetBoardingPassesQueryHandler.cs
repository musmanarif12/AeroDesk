using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.BoardingPasses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.BoardingPasses.Queries.GetBoardingPasses
{
    public class GetBoardingPassesQueryHandler
        : IRequestHandler<GetBoardingPassesQuery, List<BoardingPassDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetBoardingPassesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BoardingPassDto>> Handle(
            GetBoardingPassesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.BoardingPasses
                .AsNoTracking()
                .Select(x => new BoardingPassDto
                {
                    Id = x.Id,
                    BoardingPassNumber = x.BoardingPassNumber,
                    SeatNumber = x.SeatNumber,
                    BoardingTime = x.BoardingTime,
                    QRCode = x.QRCode,
                    Status = x.Status,
                    CheckInId = x.CheckInId
                })
                .ToListAsync(cancellationToken);
        }
    }
}