using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Aircrafts.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Aircrafts.Queries.GetAircraftById
{
    public class GetAircraftByIdQueryHandler
        : IRequestHandler<GetAircraftByIdQuery, AircraftDto?>
    {

        private readonly IApplicationDbContext _context;


        public GetAircraftByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<AircraftDto?> Handle(
            GetAircraftByIdQuery request,
            CancellationToken cancellationToken)
        {

            return await _context.Aircrafts
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new AircraftDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Model = x.Model,
                    Manufacturer = x.Manufacturer,
                    Capacity = x.Capacity,
                    RegistrationNumber = x.RegistrationNumber,
                    AirlineId = x.AirlineId
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}