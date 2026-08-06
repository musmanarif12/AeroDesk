using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
    {
        public void Configure(EntityTypeBuilder<Aircraft> builder)
        {
            builder.ToTable("Aircrafts");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Model).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Manufacturer).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Capacity).IsRequired();
            builder.Property(a => a.RegistrationNumber).IsRequired().HasMaxLength(20);
            builder.HasIndex(a => a.RegistrationNumber).IsUnique();
            builder.HasOne(a => a.Airline)
                .WithMany(al => al.Aircrafts)
                .HasForeignKey(a => a.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.Flights)
                .WithOne(f => f.Aircraft)
                .HasForeignKey(f => f.AircraftId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
