using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Airport : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string IATACode { get; set; } = string.Empty;
        public string ICAOCode { get; set; } = string.Empty;

        //Navigation Properties
        public ICollection<Gate> Gates { get; set; } = new List<Gate>();
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}


