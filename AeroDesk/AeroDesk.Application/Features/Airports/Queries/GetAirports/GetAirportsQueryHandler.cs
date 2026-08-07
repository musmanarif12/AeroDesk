using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Airports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Airports.Queries.GetAirports
{
    public class GetAirportsQueryHandler
        : IRequestHandler<GetAirportsQuery, List<AirportDto>>
    {
        private readonly IApplicationDbContext _context;


        public GetAirportsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<AirportDto>> Handle(
            GetAirportsQuery request,
            CancellationToken cancellationToken)
        {
            var airports = await _context.Airports
                .AsNoTracking()
                .Select(a => new AirportDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    IATACode = a.IATACode,
                    ICAOCode = a.ICAOCode,
                    City = a.City,
                    Country = a.Country
                })
                .ToListAsync(cancellationToken);


            return airports;
        }
    }
}