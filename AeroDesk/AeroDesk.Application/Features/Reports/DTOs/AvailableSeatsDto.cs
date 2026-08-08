namespace AeroDesk.Application.Features.Reports.DTOs
{
    public class AvailableSeatsDto
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int BookedSeats { get; set; }
        public int AvailableSeats { get; set; }
    }
}