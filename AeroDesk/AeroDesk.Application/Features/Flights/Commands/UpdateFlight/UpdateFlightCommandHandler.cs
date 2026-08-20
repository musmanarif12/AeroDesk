using System.Net.Http.Json;
using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Flights.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Flights.Commands.UpdateFlight
{
    public class UpdateFlightCommandHandler
        : IRequestHandler<UpdateFlightCommand, FlightDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateFlightCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FlightDto?> Handle(
            UpdateFlightCommand request,
            CancellationToken cancellationToken)
        {
            var flight = await _context.Flights
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (flight == null)
            {
                return null;
            }

            _mapper.Map(request, flight);

            flight.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            // --- Direct HTTP Ingestion to FlightsAnalytics API ---
            try
            {
                var client = _httpClientFactory.CreateClient();

                var payload = new
                {
                    FlightId = flight.Id,
                    FlightNumber = flight.FlightNumber,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,
                    Status = flight.Status.ToString(),
                    DepartureAirportId = flight.DepartureAirportId,
                    ArrivalAirportId = flight.ArrivalAirportId,
                    DelayMinutes = flight.ArrivalTime > flight.DepartureTime
                        ? (int)(flight.ArrivalTime - flight.DepartureTime).TotalMinutes
                        : 0,
                    UpdatedAtUtc = DateTime.UtcNow
                };

                await client.PostAsJsonAsync("http://localhost:7001/api/metrics/ingest", payload, cancellationToken);
            }
            catch (Exception)
            {
                // Analytics service down hone par core AeroDesk operation fail na ho
            }

            return _mapper.Map<FlightDto>(flight);
        }
    }
}