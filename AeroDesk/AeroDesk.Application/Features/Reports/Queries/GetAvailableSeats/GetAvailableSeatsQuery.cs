using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetAvailableSeats
{
    public class GetAvailableSeatsQuery : IRequest<List<AvailableSeatsDto>>
    {
    }
}