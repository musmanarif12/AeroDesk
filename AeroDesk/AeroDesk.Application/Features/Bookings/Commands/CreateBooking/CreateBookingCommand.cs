using AeroDesk.Application.Features.Bookings.DTOs;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Bookings.Commands.CreateBooking
{
    [Authorize(Roles = "Administrator,Check-In Officer,Passenger")]
    public class CreateBookingCommand : IRequest<BookingDto>
    {
        public string BookingReference { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string TravelClass { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int PassengerId { get; set; }
        public int FlightId { get; set; }
    }
}