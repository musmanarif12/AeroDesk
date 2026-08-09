using AeroDesk.Application.Features.Airports.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Airports.Commands.CreateAirport
{
    [Authorize(Roles = "Administrator")]
    public class CreateAirportCommand : IRequest<AirportDto>
    {
        public string Name { get; set; } = string.Empty;
        public string IATACode { get; set; } = string.Empty;
        public string ICAOCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}