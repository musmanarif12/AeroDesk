using AeroDesk.Application.Features.BoardingPasses.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.BoardingPasses.Queries.GetBoardingPassById
{
    public class GetBoardingPassByIdQuery
        : IRequest<BoardingPassDto?>
    {
        public int Id { get; set; }

        public GetBoardingPassByIdQuery(int id)
        {
            Id = id;
        }
    }
}