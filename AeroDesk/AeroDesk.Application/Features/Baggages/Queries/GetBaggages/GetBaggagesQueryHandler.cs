using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Baggages.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Baggages.Queries.GetBaggages
{
    public class GetBaggagesQueryHandler
        : IRequestHandler<GetBaggagesQuery, List<BaggageDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetBaggagesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BaggageDto>> Handle(
            GetBaggagesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Baggages
                .AsNoTracking()
                .Select(x => new BaggageDto
                {
                    Id = x.Id,
                    Weight = x.Weight,
                    TagNumber = x.TagNumber,
                    Status = x.Status,
                    PassengerId = x.PassengerId
                })
                .ToListAsync(cancellationToken);
        }
    }
}