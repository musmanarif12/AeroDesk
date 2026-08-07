using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.BoardingPasses.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.BoardingPasses.Commands.CreateBoardingPass
{
    public class CreateBoardingPassCommandHandler
        : IRequestHandler<CreateBoardingPassCommand, BoardingPassDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateBoardingPassCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BoardingPassDto> Handle(
            CreateBoardingPassCommand request,
            CancellationToken cancellationToken)
        {
            var boardingPass = _mapper.Map<BoardingPass>(request);

            _context.BoardingPasses.Add(boardingPass);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BoardingPassDto>(boardingPass);
        }
    }
}