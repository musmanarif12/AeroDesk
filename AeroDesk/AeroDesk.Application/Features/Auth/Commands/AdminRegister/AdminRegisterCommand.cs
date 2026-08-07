using AeroDesk.Application.Features.Auth.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Auth.Commands.AdminRegister
{
    public class AdminRegisterCommand
    : IRequest<RegisterResponseDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}