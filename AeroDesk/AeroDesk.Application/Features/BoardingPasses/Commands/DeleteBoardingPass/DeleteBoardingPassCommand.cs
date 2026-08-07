using MediatR;

namespace AeroDesk.Application.Features.BoardingPasses.Commands.DeleteBoardingPass
{
    public class DeleteBoardingPassCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteBoardingPassCommand(int id)
        {
            Id = id;
        }
    }
}