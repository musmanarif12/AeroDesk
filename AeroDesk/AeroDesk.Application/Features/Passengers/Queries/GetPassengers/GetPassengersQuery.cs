using AeroDesk.Application.Common.Security;
using AeroDesk.Application.Features.Passengers.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Passengers.Queries.GetPassengers
{
    [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer,Boarding Officer,BoardingOfficer")]
    public class GetPassengersQuery : IRequest<List<PassengerDto>>
    {
    }
}