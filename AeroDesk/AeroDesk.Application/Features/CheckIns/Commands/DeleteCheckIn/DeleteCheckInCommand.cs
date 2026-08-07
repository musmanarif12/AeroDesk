using MediatR;

namespace AeroDesk.Application.Features.CheckIns.Commands.DeleteCheckIn
{
    public class DeleteCheckInCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCheckInCommand(int id)
        {
            Id = id;
        }
    }
}