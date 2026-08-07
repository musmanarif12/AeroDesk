using MediatR;

namespace AeroDesk.Application.Features.Baggages.Commands.DeleteBaggage
{
    public class DeleteBaggageCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteBaggageCommand(int id)
        {
            Id = id;
        }
    }
}