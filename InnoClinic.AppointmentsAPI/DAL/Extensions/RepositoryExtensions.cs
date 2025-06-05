using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Extensions;

[ExcludeFromCodeCoverage]
public static class RepositoryExtensions
{
    public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IAppointmentResultRepository, AppointmentResultRepository>();

        return services;
    }
}