using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetFlightsByAirport
{
    public class GetFlightsByAirportQuery : IRequest<List<FlightReportDto>>
    {
        public int AirportId { get; set; }
    }
}