using AeroDesk.Application.Features.Gates.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Gates.Commands.UpdateGate
{
    public class UpdateGateCommand : IRequest<GateDto?>
    {
        public int Id { get; set; }

        public string GateNumber { get; set; } = string.Empty;

        public string Terminal { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int AirportId { get; set; }
    }
}