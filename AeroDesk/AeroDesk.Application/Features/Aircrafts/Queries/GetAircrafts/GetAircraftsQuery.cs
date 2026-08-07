using AeroDesk.Application.Features.Aircrafts.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Aircrafts.Queries.GetAircrafts
{
    public class GetAircraftsQuery : IRequest<List<AircraftDto>>
    {
    }
}