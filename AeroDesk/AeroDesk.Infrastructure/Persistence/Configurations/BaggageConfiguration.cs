using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class BaggageConfiguration : IEntityTypeConfiguration<Baggage>
    {
        public void Configure(EntityTypeBuilder<Baggage> builder)
        {
            builder.ToTable("Baggages");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Weight)
                .IsRequired()
                .HasPrecision(6, 2);

            builder.Property(b => b.TagNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(b => b.TagNumber)
                .IsUnique();

            builder.HasOne(b => b.Passenger)
                .WithMany(p => p.Baggages)
                .HasForeignKey(b => b.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}