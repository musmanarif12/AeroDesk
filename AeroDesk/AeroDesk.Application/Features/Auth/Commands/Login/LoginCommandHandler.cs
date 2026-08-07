using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(
            IApplicationDbContext context,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(
                    u => u.Email == request.Email,
                    cancellationToken);

            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }

            var isPasswordValid = _passwordHasher.VerifyPassword(
                request.Password,
                user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Success = true,
                Message = "Login successful.",
                Token = token,
                Role = user.Role.Name
            };
        }
    }
}