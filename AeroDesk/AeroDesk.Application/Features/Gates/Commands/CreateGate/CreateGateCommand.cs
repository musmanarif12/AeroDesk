using AeroDesk.Application.Features.Gates.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Gates.Commands.CreateGate
{
    [Authorize(Roles = "Administrator,Airline Manager")]
    public class CreateGateCommand : IRequest<GateDto>
    {
        public string GateNumber { get; set; } = string.Empty;
        public string Terminal { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int AirportId { get; set; }
    }
}