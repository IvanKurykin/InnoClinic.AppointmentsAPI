using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public sealed class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable("Offices");

        builder.HasKey(d => d.Id);

        builder.HasMany(o => o.Appointments)
              .WithOne(a => a.Office)
              .HasForeignKey(a => a.OfficeId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}