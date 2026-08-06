using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline>builder)
        {
            builder.ToTable("Airlines");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Code).IsRequired().HasMaxLength(10);
            builder.Property(a => a.Country).IsRequired().HasMaxLength(100);
            builder.Property(a => a.ContactNumber).IsRequired().HasMaxLength(20);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
            builder.HasIndex(a => a.Code).IsUnique();
            builder.HasIndex(a => a.Email).IsUnique();
            builder.HasMany(a => a.Aircrafts)
                .WithOne(ac => ac.Airline)
                .HasForeignKey(ac => ac.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.Flights)
                .WithOne(f => f.Airline)
                .HasForeignKey(f => f.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
