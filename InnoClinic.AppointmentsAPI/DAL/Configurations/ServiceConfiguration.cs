using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");

        builder.HasKey(d => d.Id);

        builder.HasMany(s => s.Appointments)
              .WithOne(a => a.Service)
              .HasForeignKey(a => a.ServiceId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}