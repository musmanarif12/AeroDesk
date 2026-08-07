using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Aircrafts.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Aircrafts.Queries.GetAircrafts
{
    public class GetAircraftsQueryHandler
        : IRequestHandler<GetAircraftsQuery, List<AircraftDto>>
    {

        private readonly IApplicationDbContext _context;


        public GetAircraftsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<AircraftDto>> Handle(
            GetAircraftsQuery request,
            CancellationToken cancellationToken)
        {

            return await _context.Aircrafts
                .AsNoTracking()
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
                .ToListAsync(cancellationToken);
        }
    }
}