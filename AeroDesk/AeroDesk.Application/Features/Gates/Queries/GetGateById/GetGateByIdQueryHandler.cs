using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Gates.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Gates.Queries.GetGateById
{
    public class GetGateByIdQueryHandler
        : IRequestHandler<GetGateByIdQuery, GateDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetGateByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GateDto?> Handle(
            GetGateByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Gates
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new GateDto
                {
                    Id = x.Id,
                    GateNumber = x.GateNumber,
                    Terminal = x.Terminal,
                    Status = x.Status,
                    AirportId = x.AirportId
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}