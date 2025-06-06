using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Office;
using MassTransit;

namespace BLL.Consumers.OfficeConsumers;

public class OfficeDeletedConsumer(IOfficeRepository officeRepository, IMapper mapper) : IConsumer<OfficeDeletedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OfficeDeletedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var office = mapper.Map<Office>(integrationEvent);

        await officeRepository.DeleteAsync(office);
    }
}