using AeroDesk.Application.Features.Baggages.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Baggages.Queries.GetBaggages
{
    public class GetBaggagesQuery : IRequest<List<BaggageDto>>
    {
    }
}