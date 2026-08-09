using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Features.Bookings.Commands.DeleteBooking
{
    [Authorize(Roles = "Administrator,Check-In Officer")]
    public class DeleteBookingCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteBookingCommand(int id)
        {
            Id = id;
        }
    }
}