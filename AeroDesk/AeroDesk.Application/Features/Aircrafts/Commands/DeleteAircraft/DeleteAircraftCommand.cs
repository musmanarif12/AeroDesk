using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Aircrafts.Commands.DeleteAircraft
{
    [Authorize(Roles = "Administrator")]
    public class DeleteAircraftCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAircraftCommand(int id)
        {
            Id = id;
        }
    }
}