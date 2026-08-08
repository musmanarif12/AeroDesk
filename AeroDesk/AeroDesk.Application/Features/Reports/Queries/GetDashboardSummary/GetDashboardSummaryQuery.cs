using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;

namespace AeroDesk.Application.Features.Reports.Queries.GetDashboardSummary
{
    public class GetDashboardSummaryQuery : IRequest<DashboardSummaryDto>
    {
    }
}