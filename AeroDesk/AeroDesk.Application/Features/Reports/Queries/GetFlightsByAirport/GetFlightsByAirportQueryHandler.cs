using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Reports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Reports.Queries.GetFlightsByAirport
{
    public class GetFlightsByAirportQueryHandler
        : IRequestHandler<GetFlightsByAirportQuery, List<FlightReportDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFlightsByAirportQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FlightReportDto>> Handle(
            GetFlightsByAirportQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Flights
                .AsNoTracking()
                .Where(f => f.DepartureAirportId == request.AirportId
                         || f.ArrivalAirportId == request.AirportId)
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