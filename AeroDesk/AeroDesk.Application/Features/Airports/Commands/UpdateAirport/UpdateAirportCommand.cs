using AeroDesk.Application.Features.Airports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Airports.Commands.UpdateAirport
{
    public class UpdateAirportCommand : IRequest<AirportDto?>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string IATACode { get; set; } = string.Empty;

        public string ICAOCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
    }
}