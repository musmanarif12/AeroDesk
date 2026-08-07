using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.CheckIns.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.CheckIns.Commands.UpdateCheckIn
{
    public class UpdateCheckInCommandHandler
        : IRequestHandler<UpdateCheckInCommand, CheckInDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCheckInCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CheckInDto?> Handle(
            UpdateCheckInCommand request,
            CancellationToken cancellationToken)
        {
            var checkIn = await _context.CheckIns
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (checkIn == null)
            {
                return null;
            }

            _mapper.Map(request, checkIn);

            checkIn.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CheckInDto>(checkIn);
        }
    }
}