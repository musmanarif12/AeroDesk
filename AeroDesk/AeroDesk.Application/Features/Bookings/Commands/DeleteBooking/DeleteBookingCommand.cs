using MediatR;

namespace AeroDesk.Application.Features.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteBookingCommand(int id)
        {
            Id = id;
        }
    }
}