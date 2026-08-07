namespace AeroDesk.Application.Features.Flights.DTOs
{
    public class FlightDto
    {
        public int Id { get; set; }

        public string FlightNumber { get; set; } = string.Empty;

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string Status { get; set; } = string.Empty;

        public int DepartureAirportId { get; set; }

        public int ArrivalAirportId { get; set; }

        public int GateId { get; set; }

        public int AirlineId { get; set; }

        public int AircraftId { get; set; }
    }
}