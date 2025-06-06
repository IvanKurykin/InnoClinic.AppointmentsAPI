using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Doctor;
using MassTransit;

namespace BLL.Consumers.DoctorConsumers;

public class DoctorUpdatedConsumer(IDoctorRepository doctorRepository, IMapper mapper) : IConsumer<DoctorUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<DoctorUpdatedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var doctor = mapper.Map<Doctor>(integrationEvent);

        await doctorRepository.UpdateAsync(doctor);
    }
}