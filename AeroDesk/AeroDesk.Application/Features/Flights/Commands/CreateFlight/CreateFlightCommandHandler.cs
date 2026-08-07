using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Flights.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.Flights.Commands.CreateFlight
{
    public class CreateFlightCommandHandler
        : IRequestHandler<CreateFlightCommand, FlightDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateFlightCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FlightDto> Handle(
            CreateFlightCommand request,
            CancellationToken cancellationToken)
        {
            var flight = _mapper.Map<Flight>(request);

            _context.Flights.Add(flight);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FlightDto>(flight);
        }
    }
}