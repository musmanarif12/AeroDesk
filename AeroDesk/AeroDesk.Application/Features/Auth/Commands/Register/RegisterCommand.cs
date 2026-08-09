using AeroDesk.Application.Features.Auth.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterResponseDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Passenger"; // Default Passenger
    }
}