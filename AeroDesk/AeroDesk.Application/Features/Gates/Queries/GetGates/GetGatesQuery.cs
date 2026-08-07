using AeroDesk.Application.Features.Gates.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Gates.Queries.GetGates
{
    public class GetGatesQuery : IRequest<List<GateDto>>
    {
    }
}