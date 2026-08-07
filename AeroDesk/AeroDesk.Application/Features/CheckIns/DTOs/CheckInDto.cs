namespace AeroDesk.Application.Features.CheckIns.DTOs
{
    public class CheckInDto
    {
        public int Id { get; set; }

        public DateTime CheckInTime { get; set; }

        public int BaggageCount { get; set; }

        public string Status { get; set; } = string.Empty;

        public int PassengerId { get; set; }

        public int BookingId { get; set; }

        public int FlightId { get; set; }
    }
}