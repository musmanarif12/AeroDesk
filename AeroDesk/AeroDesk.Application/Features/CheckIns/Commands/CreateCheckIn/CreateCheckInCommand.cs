using AeroDesk.Application.Features.CheckIns.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.CheckIns.Commands.CreateCheckIn
{
    public class CreateCheckInCommand : IRequest<CheckInDto>
    {
        public DateTime CheckInTime { get; set; }

        public int BaggageCount { get; set; }

        public string Status { get; set; } = string.Empty;

        public int PassengerId { get; set; }

        public int BookingId { get; set; }

        public int FlightId { get; set; }
    }
}