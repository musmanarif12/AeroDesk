using AeroDesk.Application.Features.Baggages.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Baggages.Commands.UpdateBaggage
{
    [Authorize(Roles = "Administrator,Check-In Officer")]
    public class UpdateBaggageCommand : IRequest<BaggageDto?>
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public string TagNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int PassengerId { get; set; }
    }
}