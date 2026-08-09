using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Passengers.Commands.DeletePassenger
{
    [Authorize(Roles = "Administrator")]
    public class DeletePassengerCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeletePassengerCommand(int id)
        {
            Id = id;
        }
    }
}