using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Office;
using MassTransit;

namespace BLL.Consumers.OfficeConsumers;

public class OfficeCreatedConsumer(IOfficeRepository officeRepository, IMapper mapper) : IConsumer<OfficeCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OfficeCreatedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var office = mapper.Map<Office>(integrationEvent);

        await officeRepository.CreateAsync(office);
    }
}