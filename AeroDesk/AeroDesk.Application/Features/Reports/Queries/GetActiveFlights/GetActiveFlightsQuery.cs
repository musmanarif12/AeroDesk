using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetActiveFlights
{
    public class GetActiveFlightsQuery : IRequest<List<FlightReportDto>>
    {
    }
}