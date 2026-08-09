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
            // 1. Check if Email already exists
            if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            // 2. Fetch target Role from Database dynamically
            var targetRoleName = string.IsNullOrWhiteSpace(request.Role) ? "Passenger" : request.Role;

            var selectedRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == targetRoleName, cancellationToken);

            if (selectedRole == null)
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = $"Role '{targetRoleName}' not found in system."
                };
            }

            int? createdPassengerId = null;

            // 3. Create Passenger record ONLY if registering a Passenger
            if (string.Equals(selectedRole.Name, "Passenger", StringComparison.OrdinalIgnoreCase))
            {
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
                createdPassengerId = passenger.Id;
            }

            // 4. Create User entity and link RoleId & PassengerId
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                RoleId = selectedRole.Id,
                PassengerId = createdPassengerId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new RegisterResponseDto
            {
                Success = true,
                Message = $"{selectedRole.Name} registered successfully."
            };
        }
    }
}