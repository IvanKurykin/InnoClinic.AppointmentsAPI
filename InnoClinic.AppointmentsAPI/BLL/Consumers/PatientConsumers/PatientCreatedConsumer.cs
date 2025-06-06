using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Patient;
using MassTransit;

namespace BLL.Consumers.PatientConsumers;

public class PatientCreatedConsumer(IPatientRepository patientRepository, IMapper mapper) : IConsumer<PatientCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PatientCreatedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var patient = mapper.Map<Patient>(integrationEvent);

        await patientRepository.CreateAsync(patient);
    }
}