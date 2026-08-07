namespace AeroDesk.Application.Features.Gates.DTOs
{
    public class GateDto
    {
        public int Id { get; set; }

        public string GateNumber { get; set; } = string.Empty;

        public string Terminal { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int AirportId { get; set; }
    }
}