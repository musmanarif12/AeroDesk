using MediatR;

namespace AeroDesk.Application.Features.Aircrafts.Commands.DeleteAircraft
{
    public class DeleteAircraftCommand : IRequest<bool>
    {
        public int Id { get; set; }


        public DeleteAircraftCommand(int id)
        {
            Id = id;
        }
    }
}