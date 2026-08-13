using AeroDesk.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AeroDesk.Infrastructure.BackgroundServices
{
    public class FlightNotificationBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FlightNotificationBackgroundService> _logger;

        private static readonly TimeSpan PollingInterval = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan NotificationWindowStart = TimeSpan.FromHours(4) + TimeSpan.FromMinutes(50); // 4h50m
        private static readonly TimeSpan NotificationWindowEnd = TimeSpan.FromHours(5) + TimeSpan.FromMinutes(10);   // 5h10m

        public FlightNotificationBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<FlightNotificationBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Flight Notification Background Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessFlightNotificationsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing flight notifications.");
                }

                await Task.Delay(PollingInterval, stoppingToken);
            }
        }

        private async Task ProcessFlightNotificationsAsync(CancellationToken cancellationToken)
        {
            // Create a scope to resolve scoped services (DbContext, EmailService)
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var now = DateTime.UtcNow;
            var windowStart = now + NotificationWindowStart;
            var windowEnd = now + NotificationWindowEnd;

            var flightsToNotify = await context.Flights
                .Where(f => !f.NotificationSent
                         && f.DepartureTime >= windowStart
                         && f.DepartureTime <= windowEnd)
                .ToListAsync(cancellationToken);

            if (flightsToNotify.Count == 0)
            {
                _logger.LogInformation("No flights found requiring notification at {Time}.", now);
                return;
            }

            foreach (var flight in flightsToNotify)
            {
                var bookings = await context.Bookings
                    .Where(b => b.FlightId == flight.Id && b.Status != "Cancelled")
                    .ToListAsync(cancellationToken);

                foreach (var booking in bookings)
                {
                    var passenger = await context.Passengers
                        .FirstOrDefaultAsync(p => p.Id == booking.PassengerId, cancellationToken);

                    if (passenger == null || string.IsNullOrWhiteSpace(passenger.Email))
                        continue;

                    var subject = $"Flight Reminder: {flight.FlightNumber} departs in 5 hours";
                    var body = BuildEmailBody(passenger.Name, flight.FlightNumber, flight.DepartureTime, booking.SeatNumber);

                    try
                    {
                        await emailService.SendEmailAsync(passenger.Email, subject, body, cancellationToken);
                        _logger.LogInformation(
                            "Notification email sent to {Email} for flight {FlightNumber}.",
                            passenger.Email, flight.FlightNumber);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex,
                            "Failed to send email to {Email} for flight {FlightNumber}.",
                            passenger.Email, flight.FlightNumber);
                    }
                }

                flight.NotificationSent = true;
                flight.UpdatedAt = DateTime.UtcNow;
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        private static string BuildEmailBody(string passengerName, string flightNumber, DateTime departureTime, string seatNumber)
        {
            return $@"
                <div style='font-family: Arial, sans-serif; max-width: 500px;'>
                    <h2>Flight Departure Reminder</h2>
                    <p>Dear {passengerName},</p>
                    <p>This is a reminder that your flight <strong>{flightNumber}</strong> is departing in approximately 5 hours.</p>
                    <table style='margin-top: 10px;'>
                        <tr><td><strong>Flight Number:</strong></td><td>{flightNumber}</td></tr>
                        <tr><td><strong>Departure Time:</strong></td><td>{departureTime:dddd, dd MMM yyyy - HH:mm} UTC</td></tr>
                        <tr><td><strong>Seat Number:</strong></td><td>{seatNumber}</td></tr>
                    </table>
                    <p style='margin-top: 20px;'>Please ensure you arrive at the airport with sufficient time for check-in and security.</p>
                    <p>Safe travels,<br/>AeroDesk Airport Operations</p>
                </div>";
        }
    }
}