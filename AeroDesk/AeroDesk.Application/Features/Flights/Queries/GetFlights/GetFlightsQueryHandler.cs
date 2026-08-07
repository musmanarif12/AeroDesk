using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Flights.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Flights.Queries.GetFlights
{
    public class GetFlightsQueryHandler
        : IRequestHandler<GetFlightsQuery, List<FlightDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFlightsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FlightDto>> Handle(
            GetFlightsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Flights
                .AsNoTracking()
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
        }
    }
}