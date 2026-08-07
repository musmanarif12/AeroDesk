using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Airports.DTOs;
using AeroDesk.Domain.Entities;
using MediatR;

namespace AeroDesk.Application.Features.Airports.Commands.CreateAirport
{
    public class CreateAirportCommandHandler
        : IRequestHandler<CreateAirportCommand, AirportDto>
    {
        private readonly IApplicationDbContext _context;

        public CreateAirportCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<AirportDto> Handle(
            CreateAirportCommand request,
            CancellationToken cancellationToken)
        {
            var airport = new Airport
            {
                Name = request.Name,
                IATACode = request.IATACode,
                ICAOCode = request.ICAOCode,
                City = request.City,
                Country = request.Country
            };

            // DTO → Entity conversion
            //var airport = _mapper.Map<Airport>(request);

            _context.Airports.Add(airport);

            await _context.SaveChangesAsync(cancellationToken);


            return new AirportDto
            {
                Id = airport.Id,
                Name = airport.Name,
                IATACode = airport.IATACode,
                ICAOCode = airport.ICAOCode,
                City = airport.City,
                Country = airport.Country
            };

            // Entity → DTO conversion
            //return _mapper.Map<AirportDto>(airport);
        }
    }
}