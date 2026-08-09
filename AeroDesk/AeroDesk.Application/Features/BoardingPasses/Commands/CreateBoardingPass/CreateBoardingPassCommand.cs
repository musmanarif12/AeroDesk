using AeroDesk.Application.Features.BoardingPasses.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.BoardingPasses.Commands.CreateBoardingPass
{
    [Authorize(Roles = "Administrator,Check-In Officer")]
    public class CreateBoardingPassCommand : IRequest<BoardingPassDto>
    {
        public string BoardingPassNumber { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public DateTime BoardingTime { get; set; }
        public string QRCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CheckInId { get; set; }
    }
}