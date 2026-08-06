using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Aircraft : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public int AirlineId { get; set; }

        //Navigation Properties
        public Airline Airline { get; set; } = null!;
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}
