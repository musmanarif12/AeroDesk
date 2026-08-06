using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable("Flights");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.FlightNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(f => f.DepartureTime)
                .IsRequired();

            builder.Property(f => f.ArrivalTime)
                .IsRequired();

            builder.Property(f => f.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(f => f.FlightNumber)
                .IsUnique();

            // Departure Airport (One Airport -> Many Flights)
            builder.HasOne(f => f.DepartureAirport)
                .WithMany(a => a.DepartureFlights)
                .HasForeignKey(f => f.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            // Arrival Airport (One Airport -> Many Flights)
            builder.HasOne(f => f.ArrivalAirport)
                .WithMany(a => a.ArrivalFlights)
                .HasForeignKey(f => f.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            // Gate (One Gate -> Many Flights)
            builder.HasOne(f => f.Gate)
                .WithMany(g => g.Flights)
                .HasForeignKey(f => f.GateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Airline (One Airline -> Many Flights)
            builder.HasOne(f => f.Airline)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            // Aircraft (One Aircraft -> Many Flights)
            builder.HasOne(f => f.Aircraft)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AircraftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Flight -> Bookings
            builder.HasMany(f => f.Bookings)
                .WithOne(b => b.Flight)
                .HasForeignKey(b => b.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

          
        }
    }
}