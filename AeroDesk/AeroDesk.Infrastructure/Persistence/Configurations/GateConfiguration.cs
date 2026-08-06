using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class GateConfiguration : IEntityTypeConfiguration<Gate>
    {
        public void Configure(EntityTypeBuilder<Gate> builder)
        {
            builder.ToTable("Gates");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.GateNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(g => g.Terminal)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(g => g.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(g => new { g.AirportId, g.GateNumber })
                .IsUnique();

            builder.HasOne(g => g.Airport)
                .WithMany(a => a.Gates)
                .HasForeignKey(g => g.AirportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.Flights)
                .WithOne(f => f.Gate)
                .HasForeignKey(f => f.GateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
