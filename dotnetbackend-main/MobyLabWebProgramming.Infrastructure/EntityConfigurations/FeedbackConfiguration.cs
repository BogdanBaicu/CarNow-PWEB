using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Description)
            .HasMaxLength(5000)
            .IsRequired();
        builder.Property(e => e.CarRating)
            .IsRequired();
        builder.Property(e => e.EmployeeRating)
            .IsRequired();
        builder.HasOne(e => e.Reservation)
            .WithOne()
            .HasForeignKey<Feedback>(e => e.ReservationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}
