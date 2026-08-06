using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace AeroDesk.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Airport> Airports { get; }
        DbSet<Airline> Airlines { get; }
        DbSet<Aircraft> Aircrafts { get; }
        DbSet<Baggage> Baggages { get; }
        DbSet<BoardingPass> BoardingPasses { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<CheckIn> CheckIns { get; }
        DbSet<Flight> Flights { get; }
        DbSet<Gate> Gates { get; }
        DbSet<Passenger> Passengers { get; }
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
