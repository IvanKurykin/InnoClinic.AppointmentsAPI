using DAL.Configurations;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Appointment>? Appointments { get; set; }
    public DbSet<AppointmentResult>? AppointmentResults { get; set; }
    public DbSet<Doctor>? Doctors { get; set; }
    public DbSet<Patient>? Patients { get; set; }
    public DbSet<Service>? Services { get; set; }
    public DbSet<Office>? Offices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        modelBuilder.ApplyConfiguration(new OfficeConfiguration());
        modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
    }
}