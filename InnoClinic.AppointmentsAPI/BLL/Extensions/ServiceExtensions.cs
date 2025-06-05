using System.Diagnostics.CodeAnalysis;
using BLL.Consumers.DoctorConsumers;
using BLL.Consumers.OfficeConsumers;
using BLL.Consumers.PatientConsumers;
using BLL.Consumers.ServiceConsumers;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;
using DAL.Extensions;
using InnoClinic.Messaging.Enums;
using InnoClinic.Messaging.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessLayerServices(configuration);

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAppointmentResultService, AppointmentResultService>();
        
        services.AddDefaultMassTransit(configuration, MessageBrokerType.RabbitMQ, cfg =>
        {
            cfg.AddConsumer<ServiceCreatedConsumer>();
            cfg.AddConsumer<ServiceUpdatedConsumer>();
            cfg.AddConsumer<ServiceDeletedConsumer>();
            cfg.AddConsumer<DoctorCreatedConsumer>();
            cfg.AddConsumer<DoctorUpdatedConsumer>();
            cfg.AddConsumer<DoctorDeletedConsumer>();
            cfg.AddConsumer<PatientCreatedConsumer>();
            cfg.AddConsumer<PatientUpdatedConsumer>();
            cfg.AddConsumer<PatientDeletedConsumer>();
            cfg.AddConsumer<OfficeCreatedConsumer>();
            cfg.AddConsumer<OfficeUpdatedConsumer>();
            cfg.AddConsumer<OfficeDeletedConsumer>();
        });

        return services;
    }
}