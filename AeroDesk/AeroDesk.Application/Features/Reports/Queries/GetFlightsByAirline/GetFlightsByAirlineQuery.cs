using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetFlightsByAirline
{
    public class GetFlightsByAirlineQuery : IRequest<List<FlightReportDto>>
    {
        public int AirlineId { get; set; }
    }
}