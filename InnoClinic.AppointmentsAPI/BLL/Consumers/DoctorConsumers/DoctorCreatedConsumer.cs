using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Doctor;
using MassTransit;

namespace BLL.Consumers.DoctorConsumers;

public class DoctorCreatedConsumer(IDoctorRepository doctorRepository, IMapper mapper) : IConsumer<DoctorCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<DoctorCreatedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var doctor = mapper.Map<Doctor>(integrationEvent);

        await doctorRepository.CreateAsync(doctor);
    }
}