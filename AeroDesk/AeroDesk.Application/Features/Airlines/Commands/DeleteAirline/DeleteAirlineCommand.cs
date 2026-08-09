using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Airlines.Commands.DeleteAirline
{
    [Authorize(Roles = "Administrator")]
    public class DeleteAirlineCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAirlineCommand(int id)
        {
            Id = id;
        }
    }
}