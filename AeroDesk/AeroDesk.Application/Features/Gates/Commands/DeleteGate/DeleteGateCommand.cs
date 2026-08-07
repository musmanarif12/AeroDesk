using MediatR;

namespace AeroDesk.Application.Features.Gates.Commands.DeleteGate
{
    public class DeleteGateCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteGateCommand(int id)
        {
            Id = id;
        }
    }
}