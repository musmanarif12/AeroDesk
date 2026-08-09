using AeroDesk.Application.Common.Exceptions;
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
        private readonly ICurrentUserService _currentUserService;

        public GetBoardingPassByIdQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<BoardingPassDto?> Handle(
            GetBoardingPassByIdQuery request,
            CancellationToken cancellationToken)
        {
            var boardingPass = await _context.BoardingPasses
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new
                {
                    Dto = new BoardingPassDto
                    {
                        Id = x.Id,
                        BoardingPassNumber = x.BoardingPassNumber,
                        SeatNumber = x.SeatNumber,
                        BoardingTime = x.BoardingTime,
                        QRCode = x.QRCode,
                        Status = x.Status,
                        CheckInId = x.CheckInId
                    },
                    PassengerId = x.CheckIn.PassengerId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (boardingPass == null)
            {
                return null;
            }

            // Ownership check: Passenger can only view their own boarding pass
            if (string.Equals(_currentUserService.Role, "Passenger", StringComparison.OrdinalIgnoreCase))
            {
                if (_currentUserService.PassengerId != boardingPass.PassengerId)
                {
                    throw new ForbiddenAccessException("You can only view your own boarding pass.");
                }
            }

            return boardingPass.Dto;
        }
    }
}