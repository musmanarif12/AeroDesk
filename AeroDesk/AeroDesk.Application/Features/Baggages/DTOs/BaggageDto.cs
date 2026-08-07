namespace AeroDesk.Application.Features.Baggages.DTOs
{
    public class BaggageDto
    {
        public int Id { get; set; }

        public decimal Weight { get; set; }

        public string TagNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int PassengerId { get; set; }
    }
}