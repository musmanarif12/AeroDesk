using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Flights.Commands.DeleteFlight
{
    //[Authorize(Roles = "Administrator")]
    public class DeleteFlightCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteFlightCommand(int id)
        {
            Id = id;
        }
    }
}