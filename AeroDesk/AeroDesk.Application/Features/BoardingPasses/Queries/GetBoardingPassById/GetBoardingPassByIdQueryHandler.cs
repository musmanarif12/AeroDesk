using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.BoardingPasses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.BoardingPasses.Queries.GetBoardingPassById
{
    public class GetBoardingPassByIdQueryHandler
        : IRequestHandler<GetBoardingPassByIdQuery, BoardingPassDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetBoardingPassByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BoardingPassDto?> Handle(
            GetBoardingPassByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.BoardingPasses
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}