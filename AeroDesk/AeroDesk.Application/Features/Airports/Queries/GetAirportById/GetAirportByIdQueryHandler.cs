using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Airports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Airports.Queries.GetAirportById
{
    public class GetAirportByIdQueryHandler
        : IRequestHandler<GetAirportByIdQuery, AirportDto?>
    {
        private readonly IApplicationDbContext _context;


        public GetAirportByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<AirportDto?> Handle(
            GetAirportByIdQuery request,
            CancellationToken cancellationToken)
        {
            var airport = await _context.Airports
                .AsNoTracking()
                .Where(a => a.Id == request.Id)
                .Select(a => new AirportDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    IATACode = a.IATACode,
                    ICAOCode = a.ICAOCode,
                    City = a.City,
                    Country = a.Country
                })
                .FirstOrDefaultAsync(cancellationToken);


            return airport;
        }
    }
}