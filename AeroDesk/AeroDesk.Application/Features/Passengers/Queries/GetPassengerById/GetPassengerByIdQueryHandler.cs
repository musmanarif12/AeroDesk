using AeroDesk.Application.Common.Exceptions;
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
        private readonly ICurrentUserService _currentUserService;

        public GetPassengerByIdQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<PassengerDto?> Handle(
            GetPassengerByIdQuery request,
            CancellationToken cancellationToken)
        {
            // Ownership check: Passenger can only view their own profile
            if (string.Equals(_currentUserService.Role, "Passenger", StringComparison.OrdinalIgnoreCase))
            {
                if (_currentUserService.PassengerId != request.Id)
                {
                    throw new ForbiddenAccessException("You can only view your own profile.");
                }
            }

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