using AeroDesk.Application.Features.Gates.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Gates.Queries.GetGateById
{
    public class GetGateByIdQuery : IRequest<GateDto?>
    {
        public int Id { get; set; }

        public GetGateByIdQuery(int id)
        {
            Id = id;
        }
    }
}