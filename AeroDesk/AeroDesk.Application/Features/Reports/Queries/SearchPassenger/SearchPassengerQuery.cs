using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.SearchPassenger
{
    public class SearchPassengerQuery : IRequest<List<PassengerSearchDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}