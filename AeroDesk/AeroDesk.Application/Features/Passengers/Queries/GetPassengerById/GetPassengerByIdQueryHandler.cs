using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Passengers.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Passengers.Queries.GetPassengerById
{
    public class GetPassengerByIdQueryHandler
        : IRequestHandler<GetPassengerByIdQuery, PassengerDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetPassengerByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PassengerDto?> Handle(
            GetPassengerByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Passengers
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}