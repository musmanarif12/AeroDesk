using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Baggage> Baggages { get; set; }
        public DbSet<BoardingPass> BoardingPasses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}