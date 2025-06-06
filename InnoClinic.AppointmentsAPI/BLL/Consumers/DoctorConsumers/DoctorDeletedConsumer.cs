using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Doctor;
using MassTransit;

namespace BLL.Consumers.DoctorConsumers;

public class DoctorDeletedConsumer(IDoctorRepository doctorRepository, IMapper mapper) : IConsumer<DoctorDeletedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<DoctorDeletedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var doctor = mapper.Map<Doctor>(integrationEvent);

        await doctorRepository.DeleteAsync(doctor);
    }
}