using AeroDesk.Application.Features.CheckIns.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.CheckIns.Queries.GetCheckInById
{
    public class GetCheckInByIdQuery : IRequest<CheckInDto?>
    {
        public int Id { get; set; }

        public GetCheckInByIdQuery(int id)
        {
            Id = id;
        }
    }
}