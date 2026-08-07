using AeroDesk.Application.Features.Flights.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Flights.Queries.GetFlights
{
    public class GetFlightsQuery : IRequest<List<FlightDto>>
    {
    }
}