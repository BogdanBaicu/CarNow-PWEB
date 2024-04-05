using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Brand)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(e => e.Model)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(e => e.LicensePlate)
            .HasMaxLength(10)
            .IsRequired();
        builder.HasAlternateKey(e => e.LicensePlate);
        builder.Property(e => e.VIN)
            .HasMaxLength(17)
            .IsRequired();
        builder.HasAlternateKey(e => e.VIN);
        builder.Property(e => e.Year)
            .IsRequired();
        builder.Property(e => e.Color)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(e => e.Transmission)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(e => e.EngineCC)
            .IsRequired();
        builder.Property(e => e.PowerHP)
            .IsRequired();
        builder.Property(e => e.Price)
            .IsRequired();
        builder.Property(e => e.FuelType)
            .IsRequired();
        builder.Property(e => e.BodyType)
            .IsRequired();
        builder.Property(e => e.ImageUrl)
            .HasMaxLength(255)
            .IsRequired();
        builder.HasMany(e => e.Insurance)
            .WithOne(e => e.Car)
            .HasForeignKey(e => e.CarId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.Maintenance)
            .WithOne(e => e.Car)
            .HasForeignKey(e => e.CarId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}
