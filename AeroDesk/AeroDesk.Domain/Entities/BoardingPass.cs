using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class BoardingPass : BaseEntity
    {
        public string BoardingPassNumber { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public DateTime BoardingTime { get; set; } = DateTime.UtcNow;
        public string QRCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CheckInId { get; set; }

        //Navigatio Property
        public CheckIn CheckIn { get; set; } = null!;
    }
}
