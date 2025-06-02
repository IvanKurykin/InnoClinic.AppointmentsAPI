using BLL.Consumers.ServiceConsumers;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;
using DAL.Entities;
using DAL.Extensions;
using InnoClinic.Messaging.Enums;
using InnoClinic.Messaging.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions;

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
        });

        return services;
    }
}