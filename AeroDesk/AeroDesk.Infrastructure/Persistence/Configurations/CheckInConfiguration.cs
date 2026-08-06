using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class CheckInConfiguration : IEntityTypeConfiguration<CheckIn>
    {
        public void Configure(EntityTypeBuilder<CheckIn> builder)
        {
            builder.ToTable("CheckIns");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CheckInTime)
                .IsRequired();

            builder.Property(c => c.BaggageCount)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(c => c.Passenger)
               .WithMany(p => p.CheckIns)
               .HasForeignKey(c => c.PassengerId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Booking)
                .WithMany(b => b.CheckIns)
                .HasForeignKey(c => c.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Flight)
                .WithMany(f => f.CheckIns)
                .HasForeignKey(c => c.FlightId)
                .OnDelete(DeleteBehavior.NoAction);  
        }
    }
}