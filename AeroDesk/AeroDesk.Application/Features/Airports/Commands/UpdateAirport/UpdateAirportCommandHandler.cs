using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Airports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Airports.Commands.UpdateAirport
{
    public class UpdateAirportCommandHandler
        : IRequestHandler<UpdateAirportCommand, AirportDto?>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAirportCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<AirportDto?> Handle(
            UpdateAirportCommand request,
            CancellationToken cancellationToken)
        {
            var airport = await _context.Airports
                .FirstOrDefaultAsync(
                    a => a.Id == request.Id,
                    cancellationToken);


            if (airport == null)
            {
                return null;
            }


            airport.Name = request.Name;
            airport.IATACode = request.IATACode;
            airport.ICAOCode = request.ICAOCode;
            airport.City = request.City;
            airport.Country = request.Country;


            airport.UpdatedAt = DateTime.UtcNow;


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
        }
    }
}