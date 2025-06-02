using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configurations;

public sealed class AppointmentResultConfiguration : IEntityTypeConfiguration<AppointmentResult>
{
    public void Configure(EntityTypeBuilder<AppointmentResult> builder)
    {
        builder.ToTable("AppointmentResults");

        builder.HasKey(ar => ar.Id);

        builder.HasOne(ar => ar.Appointment)
            .WithOne(a => a.Result)
            .HasForeignKey<AppointmentResult>(ar => ar.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ar => ar.Doctor)
            .WithMany(d => d.Results)
            .HasForeignKey(ar => ar.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}