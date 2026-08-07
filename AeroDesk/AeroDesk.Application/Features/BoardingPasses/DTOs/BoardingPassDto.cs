namespace AeroDesk.Application.Features.BoardingPasses.DTOs
{
    public class BoardingPassDto
    {
        public int Id { get; set; }

        public string BoardingPassNumber { get; set; } = string.Empty;

        public string SeatNumber { get; set; } = string.Empty;

        public DateTime BoardingTime { get; set; }

        public string QRCode { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int CheckInId { get; set; }
    }
}