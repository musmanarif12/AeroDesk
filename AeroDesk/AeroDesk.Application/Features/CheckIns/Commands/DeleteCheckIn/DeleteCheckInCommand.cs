using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.CheckIns.Commands.DeleteCheckIn
{
    [Authorize(Roles = "Administrator")]
    public class DeleteCheckInCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCheckInCommand(int id)
        {
            Id = id;
        }
    }
}