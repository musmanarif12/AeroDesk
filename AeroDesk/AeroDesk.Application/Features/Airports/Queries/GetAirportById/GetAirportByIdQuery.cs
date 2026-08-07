using AeroDesk.Application.Features.Airports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Airports.Queries.GetAirportById
{
    public class GetAirportByIdQuery : IRequest<AirportDto?>
    {
        public int Id { get; set; }


        public GetAirportByIdQuery(int id)
        {
            Id = id;
        }
    }
}