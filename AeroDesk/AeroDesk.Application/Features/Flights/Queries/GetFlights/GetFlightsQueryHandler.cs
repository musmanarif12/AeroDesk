using AeroDesk.Application.Common;
using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Flights.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Flights.Queries.GetFlights
{
    public class GetFlightsQueryHandler
        : IRequestHandler<GetFlightsQuery, PagedResult<FlightDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFlightsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<FlightDto>> Handle(
            GetFlightsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Flights.AsNoTracking();

            // 1. Database Level Search Filtering
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();

                query = query.Where(x =>
                    x.FlightNumber.ToLower().Contains(term) ||
                    x.Status.ToLower().Contains(term) ||
                    (x.Airline != null && x.Airline.Name.ToLower().Contains(term)) ||
                    (x.DepartureAirport != null && x.DepartureAirport.City.ToLower().Contains(term)) ||
                    (x.ArrivalAirport != null && x.ArrivalAirport.City.ToLower().Contains(term))
                );
            }

            // 2. Filtered records ka Total count calculate hoga
            var totalCount = await query.CountAsync(cancellationToken);

            // 3. Filtered Data par Pagination Apply hogi
            var items = await query
                .OrderBy(x => x.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new FlightDto
                {
                    Id = x.Id,
                    FlightNumber = x.FlightNumber,
                    DepartureTime = x.DepartureTime,
                    ArrivalTime = x.ArrivalTime,
                    Status = x.Status,
                    DepartureAirportId = x.DepartureAirportId,
                    ArrivalAirportId = x.ArrivalAirportId,
                    GateId = x.GateId,
                    AirlineId = x.AirlineId,
                    AircraftId = x.AircraftId
                })
                .ToListAsync(cancellationToken);

            // 4. PagedResult Return karenge
            return new PagedResult<FlightDto>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }
}