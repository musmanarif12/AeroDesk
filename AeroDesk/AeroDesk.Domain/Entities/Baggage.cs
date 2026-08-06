using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Baggage : BaseEntity
    {
        public decimal Weight { get; set; }
        public string TagNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int PassengerId { get; set; }

        //Navigation Properties
        public Passenger Passenger { get; set; } = null!;
    }
}
