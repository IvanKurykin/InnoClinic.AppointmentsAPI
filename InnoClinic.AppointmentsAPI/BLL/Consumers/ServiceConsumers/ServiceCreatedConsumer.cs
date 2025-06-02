using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Service;
using MassTransit;

namespace BLL.Consumers.ServiceConsumers;

public class ServiceCreatedConsumer(IServiceRepository serviceRepository, IMapper mapper) : IConsumer<ServiceCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ServiceCreatedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var service = mapper.Map<Service>(integrationEvent);

        await serviceRepository.CreateAsync(service);
    }
}