using AeroDesk.Application.Features.Bookings.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Bookings.Queries.GetBookingById
{
    public class GetBookingByIdQuery : IRequest<BookingDto?>
    {
        public int Id { get; set; }

        public GetBookingByIdQuery(int id)
        {
            Id = id;
        }
    }
}