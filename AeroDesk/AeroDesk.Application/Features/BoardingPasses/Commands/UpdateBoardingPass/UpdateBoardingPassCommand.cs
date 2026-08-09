using AeroDesk.Application.Features.BoardingPasses.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.BoardingPasses.Commands.UpdateBoardingPass
{
    [Authorize(Roles = "Administrator,Check-In Officer,Boarding Officer")]
    public class UpdateBoardingPassCommand : IRequest<BoardingPassDto?>
    {
        public int Id { get; set; }
        public string BoardingPassNumber { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public DateTime BoardingTime { get; set; }
        public string QRCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CheckInId { get; set; }
    }
}