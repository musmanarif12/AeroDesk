namespace AeroDesk.Application.Features.Aircrafts.DTOs
{
    public class AircraftDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty;

        public int AirlineId { get; set; }
    }
}