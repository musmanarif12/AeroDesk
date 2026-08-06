using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public string BookingReference { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public string SeatNumber { get; set; } = string.Empty;
        public string TravelClass { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int PassengerId { get; set; }
        public int FlightId { get; set; }

        //Navigation Properties
        public Passenger Passenger { get; set; } = null!;
        public Flight Flight { get; set; } = null!;
        public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
 
    }
}
