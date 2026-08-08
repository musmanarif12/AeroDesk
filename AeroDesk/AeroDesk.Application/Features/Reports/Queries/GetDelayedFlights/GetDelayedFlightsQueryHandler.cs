using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Reports.Queries.GetDelayedFlights
{
    public class GetDelayedFlightsQueryHandler
        : IRequestHandler<GetDelayedFlightsQuery, List<FlightReportDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDelayedFlightsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FlightReportDto>> Handle(
            GetDelayedFlightsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Flights
                .AsNoTracking()
                .Where(f => f.Status == "Delayed")
                .OrderBy(f => f.DepartureTime)
                .Select(f => new FlightReportDto
                {
                    Id = f.Id,
                    FlightNumber = f.FlightNumber,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    Status = f.Status,

                    AirlineId = f.AirlineId,
                    AirlineName = f.Airline.Name,

                    AircraftId = f.AircraftId,
                    AircraftName = f.Aircraft.Name,

                    DepartureAirportId = f.DepartureAirportId,
                    DepartureAirportName = f.DepartureAirport.Name,

                    ArrivalAirportId = f.ArrivalAirportId,
                    ArrivalAirportName = f.ArrivalAirport.Name,

                    GateId = f.GateId,
                    GateNumber = f.Gate.GateNumber
                })
                .ToListAsync(cancellationToken);
        }
    }
}