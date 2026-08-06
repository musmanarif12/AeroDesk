using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Flight : BaseEntity
    {
        public string FlightNumber { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public int GateId { get; set; }
        public int AirlineId { get; set; }
        public int AircraftId { get; set; }

        // Navigation Properties
        public Airport DepartureAirport { get; set; } = null!;
        public Airport ArrivalAirport { get; set; } = null!;
        public Gate Gate { get; set; } = null!;
        public Airline Airline { get; set; } = null!;
        public Aircraft Aircraft { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
    }
}