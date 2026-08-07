using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Gates.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Gates.Queries.GetGates
{
    public class GetGatesQueryHandler
        : IRequestHandler<GetGatesQuery, List<GateDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetGatesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GateDto>> Handle(
            GetGatesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Gates
                .AsNoTracking()
                .Select(x => new GateDto
                {
                    Id = x.Id,
                    GateNumber = x.GateNumber,
                    Terminal = x.Terminal,
                    Status = x.Status,
                    AirportId = x.AirportId
                })
                .ToListAsync(cancellationToken);
        }
    }
}