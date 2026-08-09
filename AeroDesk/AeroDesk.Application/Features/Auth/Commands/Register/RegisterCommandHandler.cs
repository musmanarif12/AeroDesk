using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Auth.DTOs;
using AeroDesk.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(
            IApplicationDbContext context,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterResponseDto> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            // Check email
            if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            // Get Passenger Role
            var passengerRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == "Passenger", cancellationToken);

            if (passengerRole == null)
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Passenger role not found."
                };
            }

            // 1. Create Passenger record first
            var passenger = new Passenger
            {
                Name = request.Name,
                Email = request.Email,
                PassportNumber = "PENDING",
                Nationality = "N/A",
                Gender = "N/A",
                PhoneNumber = "N/A",
                DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync(cancellationToken);

            // 2. Create User and link created PassengerId
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                RoleId = passengerRole.Id,
                PassengerId = passenger.Id
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new RegisterResponseDto
            {
                Success = true,
                Message = "Passenger registered successfully."
            };
        }
    }
}