namespace AeroDesk.Application.Features.Reports.DTOs
{
    public class FlightReportDto
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; } = string.Empty;

        public int AirlineId { get; set; }
        public string AirlineName { get; set; } = string.Empty;

        public int AircraftId { get; set; }
        public string AircraftName { get; set; } = string.Empty;

        public int DepartureAirportId { get; set; }
        public string DepartureAirportName { get; set; } = string.Empty;

        public int ArrivalAirportId { get; set; }
        public string ArrivalAirportName { get; set; } = string.Empty;

        public int GateId { get; set; }
        public string GateNumber { get; set; } = string.Empty;
    }
}