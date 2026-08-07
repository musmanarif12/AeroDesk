using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Passengers.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Passengers.Commands.UpdatePassenger
{
    public class UpdatePassengerCommandHandler
        : IRequestHandler<UpdatePassengerCommand, PassengerDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePassengerCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PassengerDto?> Handle(
            UpdatePassengerCommand request,
            CancellationToken cancellationToken)
        {
            var passenger = await _context.Passengers
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (passenger == null)
            {
                return null;
            }

            _mapper.Map(request, passenger);

            passenger.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PassengerDto>(passenger);
        }
    }
}