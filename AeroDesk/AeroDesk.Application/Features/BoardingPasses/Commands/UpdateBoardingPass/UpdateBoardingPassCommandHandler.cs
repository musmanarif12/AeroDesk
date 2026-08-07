using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.BoardingPasses.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.BoardingPasses.Commands.UpdateBoardingPass
{
    public class UpdateBoardingPassCommandHandler
        : IRequestHandler<UpdateBoardingPassCommand, BoardingPassDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBoardingPassCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BoardingPassDto?> Handle(
            UpdateBoardingPassCommand request,
            CancellationToken cancellationToken)
        {
            var boardingPass = await _context.BoardingPasses
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (boardingPass == null)
            {
                return null;
            }

            _mapper.Map(request, boardingPass);

            boardingPass.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BoardingPassDto>(boardingPass);
        }
    }
}