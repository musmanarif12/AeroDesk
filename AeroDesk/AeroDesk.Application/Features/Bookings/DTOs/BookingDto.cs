namespace AeroDesk.Application.Features.Bookings.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }

        public string BookingReference { get; set; } = string.Empty;

        public DateTime BookingDate { get; set; }

        public string SeatNumber { get; set; } = string.Empty;

        public string TravelClass { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int PassengerId { get; set; }

        public int FlightId { get; set; }
    }
}