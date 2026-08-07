using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Baggages.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Baggages.Queries.GetBaggageById
{
    public class GetBaggageByIdQueryHandler
        : IRequestHandler<GetBaggageByIdQuery, BaggageDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetBaggageByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaggageDto?> Handle(
            GetBaggageByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Baggages
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new BaggageDto
                {
                    Id = x.Id,
                    Weight = x.Weight,
                    TagNumber = x.TagNumber,
                    Status = x.Status,
                    PassengerId = x.PassengerId
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}