using AeroDesk.Domain.Common;
using AeroDesk.Domain.Enums;

namespace AeroDesk.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; } 

    }
}
