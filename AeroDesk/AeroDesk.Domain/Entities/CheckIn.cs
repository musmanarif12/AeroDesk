using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class CheckIn : BaseEntity
    {
        public DateTime CheckInTime { get; set; } = DateTime.UtcNow;
        public int BaggageCount { get; set; }
        public string Status { get; set; } = string.Empty;
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        public int FlightId { get; set; }

        //Navigation Properties
        public Passenger Passenger { get; set; } = null!;
        public Booking Booking { get; set; } = null!;
        public Flight Flight { get; set; } = null!;
        public BoardingPass BoardingPass { get; set; } = null!;
    }
}
