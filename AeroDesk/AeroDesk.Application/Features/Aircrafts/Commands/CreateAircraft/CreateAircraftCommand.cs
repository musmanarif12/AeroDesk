using AeroDesk.Application.Features.Aircrafts.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Aircrafts.Commands.CreateAircraft
{
    public class CreateAircraftCommand : IRequest<AircraftDto>
    {
        public string Name { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty;

        public int AirlineId { get; set; }
    }
}