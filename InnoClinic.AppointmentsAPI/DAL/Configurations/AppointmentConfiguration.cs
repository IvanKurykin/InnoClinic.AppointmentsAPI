using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Patient)
              .WithMany(p => p.Appointments)
              .HasForeignKey(a => a.PatientId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Doctor)
              .WithMany(d => d.Appointments)
              .HasForeignKey(a => a.DoctorId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Service)
              .WithMany(s => s.Appointments)
              .HasForeignKey(a => a.ServiceId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Office)
              .WithMany(o => o.Appointments)
              .HasForeignKey(a => a.OfficeId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Result)
              .WithOne(ar => ar.Appointment)
              .HasForeignKey<AppointmentResult>(ar => ar.AppointmentId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}