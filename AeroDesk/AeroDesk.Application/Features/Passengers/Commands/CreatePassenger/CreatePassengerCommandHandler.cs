using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Passengers.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.Passengers.Commands.CreatePassenger
{
    public class CreatePassengerCommandHandler
        : IRequestHandler<CreatePassengerCommand, PassengerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePassengerCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PassengerDto> Handle(
            CreatePassengerCommand request,
            CancellationToken cancellationToken)
        {
            var passenger = _mapper.Map<Passenger>(request);

            _context.Passengers.Add(passenger);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PassengerDto>(passenger);
        }
    }
}