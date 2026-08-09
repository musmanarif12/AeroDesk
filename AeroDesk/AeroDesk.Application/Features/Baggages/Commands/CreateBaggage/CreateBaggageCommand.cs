using AeroDesk.Application.Features.Baggages.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Baggages.Commands.CreateBaggage
{
    [Authorize(Roles = "Administrator,Check-In Officer")]
    public class CreateBaggageCommand : IRequest<BaggageDto>
    {
        public decimal Weight { get; set; }
        public string TagNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int PassengerId { get; set; }
    }
}