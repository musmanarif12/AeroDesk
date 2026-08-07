using AeroDesk.Application.Features.Airlines.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Airlines.Queries.GetAirlineById
{
    public class GetAirlineByIdQuery : IRequest<AirlineDto?>
    {
        public int Id { get; set; }

        public GetAirlineByIdQuery(int id)
        {
            Id = id;
        }
    }
}