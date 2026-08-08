using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.FileName).IsRequired().HasMaxLength(255);
            builder.Property(d => d.StoredFileName).IsRequired().HasMaxLength(255);
            builder.Property(d => d.FilePath).IsRequired().HasMaxLength(500);
            builder.Property(d => d.ContentType).IsRequired().HasMaxLength(100);
            builder.Property(d => d.FileSizeBytes).IsRequired();

            builder.Property(d => d.EntityType).IsRequired().HasMaxLength(50);
            builder.Property(d => d.EntityId).IsRequired();

            builder.Property(d => d.IsDeleted).HasDefaultValue(false);

            // Fast lookup: "give me all documents for Passenger #5"
            builder.HasIndex(d => new { d.EntityType, d.EntityId });

            builder.HasOne(d => d.UploadedByUser)
                .WithMany()
                .HasForeignKey(d => d.UploadedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}