using AeroDesk.Application.Features.Flights.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Flights.Queries.GetFlightById
{
    public class GetFlightByIdQuery : IRequest<FlightDto?>
    {
        public int Id { get; set; }

        public GetFlightByIdQuery(int id)
        {
            Id = id;
        }
    }
}