using AeroDesk.Application.Features.Aircrafts.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Aircrafts.Queries.GetAircraftById
{
    public class GetAircraftByIdQuery : IRequest<AircraftDto?>
    {
        public int Id { get; set; }


        public GetAircraftByIdQuery(int id)
        {
            Id = id;
        }
    }
}