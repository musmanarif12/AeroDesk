using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Passenger : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //Navigation Properties
        public User? User { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Baggage> Baggages { get; set; } = new List<Baggage>();
        public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    }
}
