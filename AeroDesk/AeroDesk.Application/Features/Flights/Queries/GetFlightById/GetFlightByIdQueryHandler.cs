using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Flights.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Flights.Queries.GetFlightById
{
    public class GetFlightByIdQueryHandler
        : IRequestHandler<GetFlightByIdQuery, FlightDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetFlightByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FlightDto?> Handle(
            GetFlightByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Flights
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}