using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Airline : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //Navigation Property
        public ICollection<Aircraft> Aircrafts { get; set; } = new List<Aircraft>();
    }
}
