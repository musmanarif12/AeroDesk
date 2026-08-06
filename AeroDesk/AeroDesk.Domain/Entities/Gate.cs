using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Gate : BaseEntity
    {
        public string GateNumber { get; set; } = string.Empty;
        public string Terminal { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int AirportId { get; set; }

        //Navigation Properties
        public Airport Airport { get; set; } = null!;
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}
