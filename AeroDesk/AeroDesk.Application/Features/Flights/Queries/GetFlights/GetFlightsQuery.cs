using AeroDesk.Application.Common;
using AeroDesk.Application.Features.Flights.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Flights.Queries.GetFlights
{
    public class GetFlightsQuery : IRequest<PagedResult<FlightDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
        public string? SearchTerm { get; init; }
    }
}