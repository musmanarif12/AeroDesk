using AeroDesk.Application.Features.Airlines.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Airlines.Commands.CreateAirline
{
    [Authorize(Roles = "Administrator")]
    public class CreateAirlineCommand : IRequest<AirlineDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}