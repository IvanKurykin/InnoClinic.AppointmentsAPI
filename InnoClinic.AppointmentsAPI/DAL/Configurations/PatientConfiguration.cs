using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasIndex(p => p.Email).IsUnique();

        builder.HasMany(p => p.Appointments)
              .WithOne(a => a.Patient)
              .HasForeignKey(a => a.PatientId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}