using AeroDesk.Application.Features.Bookings.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommand : IRequest<BookingDto?>
    {
        public int Id { get; set; }

        public string BookingReference { get; set; } = string.Empty;

        public DateTime BookingDate { get; set; }

        public string SeatNumber { get; set; } = string.Empty;

        public string TravelClass { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int PassengerId { get; set; }

        public int FlightId { get; set; }
    }
}