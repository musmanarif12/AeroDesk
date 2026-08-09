using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Airports.Commands.DeleteAirport
{
    [Authorize(Roles = "Administrator")]
    public class DeleteAirportCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAirportCommand(int id)
        {
            Id = id;
        }
    }
}