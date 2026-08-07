using MediatR;

namespace AeroDesk.Application.Features.Airports.Commands.DeleteAirport
{
    public class DeleteAirportCommand : IRequest<bool>
    {
        public int Id { get; set; }


        public DeleteAirportCommand(int id)
        {
            Id = id;
        }
    }
}