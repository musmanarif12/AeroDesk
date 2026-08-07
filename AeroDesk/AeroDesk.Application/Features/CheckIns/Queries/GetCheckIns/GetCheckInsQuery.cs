using AeroDesk.Application.Features.CheckIns.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.CheckIns.Queries.GetCheckIns
{
    public class GetCheckInsQuery : IRequest<List<CheckInDto>>
    {
    }
}