using AeroDesk.Application.Features.Airlines.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Airlines.Commands.UpdateAirline
{
    public class UpdateAirlineCommand : IRequest<AirlineDto?>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}