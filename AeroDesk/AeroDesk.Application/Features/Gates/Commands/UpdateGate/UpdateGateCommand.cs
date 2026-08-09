using AeroDesk.Application.Features.Gates.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Gates.Commands.UpdateGate
{
    [Authorize(Roles = "Administrator,Airline Manager")]
    public class UpdateGateCommand : IRequest<GateDto?>
    {
        public int Id { get; set; }
        public string GateNumber { get; set; } = string.Empty;
        public string Terminal { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int AirportId { get; set; }
    }
}