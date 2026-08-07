using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Auth.DTOs;
using AeroDesk.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Auth.Commands.AdminRegister
{
    public class AdminRegisterCommandHandler : IRequestHandler<AdminRegisterCommand, RegisterResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AdminRegisterCommandHandler(
            IApplicationDbContext context,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterResponseDto> Handle(
            AdminRegisterCommand request,
            CancellationToken cancellationToken)
        {
            // Check Email
            if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            // Check if Administrator already exists
            bool adminExists = await _context.Users
                .Include(u => u.Role)
                .AnyAsync(u => u.Role.Name == "Administrator", cancellationToken);

            if (adminExists)
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Administrator already exists."
                };
            }

            // Get Administrator Role
            var adminRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == "Administrator", cancellationToken);

            if (adminRole == null)
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Administrator role not found."
                };
            }

            // Create Administrator
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                RoleId = adminRole.Id
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return new RegisterResponseDto
            {
                Success = true,
                Message = "Administrator registered successfully."
            };
        }
    }
}