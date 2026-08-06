using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class BoardingPassConfiguration : IEntityTypeConfiguration<BoardingPass>
    {
        public void Configure(EntityTypeBuilder<BoardingPass> builder)
        {
            builder.ToTable("BoardingPasses");

            builder.HasKey(bp => bp.Id);

            builder.Property(bp => bp.BoardingPassNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(bp => bp.SeatNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(bp => bp.BoardingTime)
                .IsRequired();

            builder.Property(bp => bp.QRCode)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(bp => bp.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(bp => bp.BoardingPassNumber)
                .IsUnique();

            builder.HasOne(bp => bp.CheckIn)
                .WithOne(c => c.BoardingPass)
                .HasForeignKey<BoardingPass>(bp => bp.CheckInId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}