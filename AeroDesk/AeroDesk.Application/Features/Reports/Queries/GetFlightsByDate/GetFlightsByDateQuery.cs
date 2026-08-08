using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetFlightsByDate
{
    public class GetFlightsByDateQuery : IRequest<List<FlightReportDto>>
    {
        public DateTime Date { get; set; }
    }
}