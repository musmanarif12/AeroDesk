using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Aircrafts.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.Aircrafts.Commands.CreateAircraft
{
    public class CreateAircraftCommandHandler
        : IRequestHandler<CreateAircraftCommand, AircraftDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public CreateAircraftCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<AircraftDto> Handle(
            CreateAircraftCommand request,
            CancellationToken cancellationToken)
        {

            var aircraft = _mapper.Map<Aircraft>(request);


            _context.Aircrafts.Add(aircraft);


            await _context.SaveChangesAsync(cancellationToken);


            return _mapper.Map<AircraftDto>(aircraft);
        }
    }
}