using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetDailyBookings
{
    public class GetDailyBookingsQuery : IRequest<List<DailyBookingsDto>>
    {
    }
}