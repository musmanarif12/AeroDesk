using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetDelayedFlights
{
    public class GetDelayedFlightsQuery : IRequest<List<FlightReportDto>>
    {
    }
}