using AeroDesk.Application.Features.Airlines.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Airlines.Queries.GetAirlines
{
    public class GetAirlinesQuery : IRequest<List<AirlineDto>>
    {
    }
}