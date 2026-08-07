using AeroDesk.Application.Features.Passengers.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Passengers.Queries.GetPassengers
{
    public class GetPassengersQuery : IRequest<List<PassengerDto>>
    {
    }
}