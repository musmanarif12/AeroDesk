using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Airlines.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Airlines.Queries.GetAirlines
{
    public class GetAirlinesQueryHandler
        : IRequestHandler<GetAirlinesQuery, List<AirlineDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAirlinesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AirlineDto>> Handle(
            GetAirlinesQuery request,
            CancellationToken cancellationToken)
        {
            var airlines = await _context.Airlines
                .AsNoTracking()
                .Select(x => new AirlineDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Country = x.Country,
                    ContactNumber = x.ContactNumber,
                    Email = x.Email
                })
                .ToListAsync(cancellationToken);

            return airlines;
        }
    }
}