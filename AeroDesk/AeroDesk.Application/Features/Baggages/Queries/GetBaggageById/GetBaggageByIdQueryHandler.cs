using AeroDesk.Application.Common.Exceptions;
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
        private readonly ICurrentUserService _currentUserService;

        public GetBaggageByIdQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<BaggageDto?> Handle(
            GetBaggageByIdQuery request,
            CancellationToken cancellationToken)
        {
            var baggage = await _context.Baggages
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

            if (baggage == null)
            {
                return null;
            }

            // Ownership check: Passenger can only track their own baggage
            if (string.Equals(_currentUserService.Role, "Passenger", StringComparison.OrdinalIgnoreCase))
            {
                if (_currentUserService.PassengerId != baggage.PassengerId)
                {
                    throw new ForbiddenAccessException("You can only view your own baggage.");
                }
            }

            return baggage;
        }
    }
}