using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.BookingReference)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.BookingDate)
                .IsRequired();

            builder.Property(b => b.SeatNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(b => b.TravelClass)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(b => b.BookingReference)
                .IsUnique();

            builder.HasOne(b => b.Passenger)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Flight)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.CheckIns)
                .WithOne(c => c.Booking)
                .HasForeignKey(c => c.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}