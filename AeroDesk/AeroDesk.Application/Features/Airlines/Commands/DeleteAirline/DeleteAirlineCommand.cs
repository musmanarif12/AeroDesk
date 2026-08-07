using MediatR;

namespace AeroDesk.Application.Features.Airlines.Commands.DeleteAirline
{
    public class DeleteAirlineCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAirlineCommand(int id)
        {
            Id = id;
        }
    }
}