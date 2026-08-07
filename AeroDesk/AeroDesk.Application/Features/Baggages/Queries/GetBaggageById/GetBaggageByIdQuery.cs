using AeroDesk.Application.Features.Baggages.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Baggages.Queries.GetBaggageById
{
    public class GetBaggageByIdQuery : IRequest<BaggageDto?>
    {
        public int Id { get; set; }

        public GetBaggageByIdQuery(int id)
        {
            Id = id;
        }
    }
}