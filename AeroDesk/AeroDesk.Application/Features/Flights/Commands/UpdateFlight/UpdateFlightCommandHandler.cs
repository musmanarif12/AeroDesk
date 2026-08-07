using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Flights.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Flights.Commands.UpdateFlight
{
    public class UpdateFlightCommandHandler
        : IRequestHandler<UpdateFlightCommand, FlightDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFlightCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FlightDto?> Handle(
            UpdateFlightCommand request,
            CancellationToken cancellationToken)
        {
            var flight = await _context.Flights
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (flight == null)
            {
                return null;
            }

            _mapper.Map(request, flight);

            flight.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FlightDto>(flight);
        }
    }
}