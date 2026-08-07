using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Airlines.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Airlines.Queries.GetAirlineById
{
    public class GetAirlineByIdQueryHandler
        : IRequestHandler<GetAirlineByIdQuery, AirlineDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetAirlineByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AirlineDto?> Handle(
            GetAirlineByIdQuery request,
            CancellationToken cancellationToken)
        {
            var airline = await _context.Airlines
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new AirlineDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Country = x.Country,
                    ContactNumber = x.ContactNumber,
                    Email = x.Email
                })
                .FirstOrDefaultAsync(cancellationToken);

            return airline;
        }
    }
}