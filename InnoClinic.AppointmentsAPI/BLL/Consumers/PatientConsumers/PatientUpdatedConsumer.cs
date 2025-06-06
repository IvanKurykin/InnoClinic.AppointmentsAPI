using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Patient;
using MassTransit;

namespace BLL.Consumers.PatientConsumers;

public class PatientUpdatedConsumer(IPatientRepository patientRepository, IMapper mapper) : IConsumer<PatientUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PatientUpdatedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var patient = mapper.Map<Patient>(integrationEvent);

        await patientRepository.UpdateAsync(patient);
    }
}