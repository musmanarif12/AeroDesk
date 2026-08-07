using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.CheckIns.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.CheckIns.Commands.CreateCheckIn
{
    public class CreateCheckInCommandHandler
        : IRequestHandler<CreateCheckInCommand, CheckInDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCheckInCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CheckInDto> Handle(
            CreateCheckInCommand request,
            CancellationToken cancellationToken)
        {
            var checkIn = _mapper.Map<CheckIn>(request);

            _context.CheckIns.Add(checkIn);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CheckInDto>(checkIn);
        }
    }
}