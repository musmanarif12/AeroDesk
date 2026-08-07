using AeroDesk.Application.Features.Passengers.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Passengers.Queries.GetPassengerById
{
    public class GetPassengerByIdQuery : IRequest<PassengerDto?>
    {
        public int Id { get; set; }

        public GetPassengerByIdQuery(int id)
        {
            Id = id;
        }
    }
}