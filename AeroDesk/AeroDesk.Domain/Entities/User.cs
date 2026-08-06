using AeroDesk.Domain.Common;


namespace AeroDesk.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; } 

    }
}
