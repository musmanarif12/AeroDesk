using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Passengers.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Passengers.Queries.GetPassengers
{
    public class GetPassengersQueryHandler
        : IRequestHandler<GetPassengersQuery, List<PassengerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetPassengersQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PassengerDto>> Handle(
            GetPassengersQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Passengers
                .AsNoTracking()
                .Select(x => new PassengerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Gender = x.Gender,
                    DateOfBirth = x.DateOfBirth,
                    PassportNumber = x.PassportNumber,
                    Nationality = x.Nationality,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email
                })
                .ToListAsync(cancellationToken);
        }
    }
}