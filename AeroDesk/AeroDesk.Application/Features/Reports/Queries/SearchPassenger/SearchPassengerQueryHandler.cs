using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Reports.Queries.SearchPassenger
{
    public class SearchPassengerQueryHandler
        : IRequestHandler<SearchPassengerQuery, List<PassengerSearchDto>>
    {
        private readonly IApplicationDbContext _context;

        public SearchPassengerQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PassengerSearchDto>> Handle(
            SearchPassengerQuery request,
            CancellationToken cancellationToken)
        {
            var term = request.SearchTerm.Trim().ToLower();

            return await _context.Passengers
                .AsNoTracking()
                .Where(p =>
                    p.Name.ToLower().Contains(term) ||
                    p.PassportNumber.ToLower().Contains(term) ||
                    p.Email.ToLower().Contains(term) ||
                    p.PhoneNumber.ToLower().Contains(term))
                .OrderBy(p => p.Name)
                .Select(p => new PassengerSearchDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Gender = p.Gender,
                    PassportNumber = p.PassportNumber,
                    Nationality = p.Nationality,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email
                })
                .ToListAsync(cancellationToken);
        }
    }
}