using AeroDesk.Application.Features.Airports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Airports.Queries.GetAirports
{
    public class GetAirportsQuery : IRequest<List<AirportDto>>
    {
    }
}