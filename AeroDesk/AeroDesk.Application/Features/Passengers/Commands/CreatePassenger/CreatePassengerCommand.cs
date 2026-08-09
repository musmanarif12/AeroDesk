using AeroDesk.Application.Features.Passengers.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Passengers.Commands.CreatePassenger
{
    [Authorize(Roles = "Administrator,Check-In Officer")]
    public class CreatePassengerCommand : IRequest<PassengerDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}