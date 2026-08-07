using AeroDesk.Application.Features.Bookings.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Bookings.Queries.GetBookings
{
    public class GetBookingsQuery : IRequest<List<BookingDto>>
    {
    }
}