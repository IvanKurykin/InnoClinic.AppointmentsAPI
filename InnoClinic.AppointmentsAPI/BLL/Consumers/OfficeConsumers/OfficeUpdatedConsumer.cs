using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Office;
using MassTransit;

namespace BLL.Consumers.OfficeConsumers;

public class OfficeUpdatedConsumer(IOfficeRepository officeRepository, IMapper mapper) : IConsumer<OfficeUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OfficeUpdatedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var office = mapper.Map<Office>(integrationEvent);

        await officeRepository.UpdateAsync(office);
    }
}