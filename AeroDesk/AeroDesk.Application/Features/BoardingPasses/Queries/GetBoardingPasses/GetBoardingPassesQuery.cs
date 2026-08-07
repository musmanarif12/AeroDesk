using AeroDesk.Application.Features.BoardingPasses.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.BoardingPasses.Queries.GetBoardingPasses
{
    public class GetBoardingPassesQuery : IRequest<List<BoardingPassDto>>
    {
    }
}