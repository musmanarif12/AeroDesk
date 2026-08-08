namespace AeroDesk.Application.Features.Reports.DTOs
{
    public class PassengerCountPerFlightDto
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public int PassengerCount { get; set; }
    }
}