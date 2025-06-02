using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Service;
using MassTransit;

namespace BLL.Consumers.ServiceConsumers;

public class ServiceUpdatedConsumer(IServiceRepository serviceRepository, IMapper mapper) : IConsumer<ServiceUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ServiceUpdatedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var service = mapper.Map<Service>(integrationEvent);

        await serviceRepository.UpdateAsync(service);
    }
}