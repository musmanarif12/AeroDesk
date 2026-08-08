namespace AeroDesk.Application.Features.Reports.DTOs
{
    public class DashboardSummaryDto
    {
        public int FlightsToday { get; set; }
        public int ActiveFlights { get; set; }
        public int TotalPassengers { get; set; }
        public int DelayedFlights { get; set; }
        public int TodayBookings { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}