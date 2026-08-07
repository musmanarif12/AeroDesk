using AeroDesk.Application.Features.Baggages.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Baggages.Commands.CreateBaggage
{
    public class CreateBaggageCommand : IRequest<BaggageDto>
    {
        public decimal Weight { get; set; }

        public string TagNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int PassengerId { get; set; }
    }
}