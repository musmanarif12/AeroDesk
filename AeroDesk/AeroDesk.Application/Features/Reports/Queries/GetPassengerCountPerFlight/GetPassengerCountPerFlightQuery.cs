using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetPassengerCountPerFlight
{
    public class GetPassengerCountPerFlightQuery : IRequest<List<PassengerCountPerFlightDto>>
    {
    }
}