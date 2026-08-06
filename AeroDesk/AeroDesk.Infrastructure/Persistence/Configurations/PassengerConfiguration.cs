using AeroDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroDesk.Infrastructure.Persistence.Configurations
{
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.ToTable("Passengers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.DateOfBirth)
                .IsRequired();

            builder.Property(p => p.PassportNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Nationality)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(p => p.PassportNumber)
                .IsUnique();

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.HasOne<User>()
                .WithOne(u => u.Passenger)
                .HasForeignKey<User>(u => u.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Bookings)
                .WithOne(b => b.Passenger)
                .HasForeignKey(b => b.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Baggages)
                .WithOne(b => b.Passenger)
                .HasForeignKey(b => b.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.CheckIns)
                .WithOne(c => c.Passenger)
                .HasForeignKey(c => c.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}