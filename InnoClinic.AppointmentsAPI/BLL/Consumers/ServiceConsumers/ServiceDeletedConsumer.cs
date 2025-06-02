using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Service;
using MassTransit;

namespace BLL.Consumers.ServiceConsumers;

public class ServiceDeletedConsumer(IServiceRepository serviceRepository, IMapper mapper) : IConsumer<ServiceDeletedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ServiceDeletedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var service = mapper.Map<Service>(integrationEvent);

        await serviceRepository.DeleteAsync(service);
    }
}