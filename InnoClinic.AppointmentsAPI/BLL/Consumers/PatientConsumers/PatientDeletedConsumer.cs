using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Patient;
using MassTransit;

namespace BLL.Consumers.PatientConsumers;

public class PatientDeletedConsumer(IPatientRepository patientRepository, IMapper mapper) : IConsumer<PatientDeletedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PatientDeletedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        var patient = mapper.Map<Patient>(integrationEvent);

        await patientRepository.DeleteAsync(patient);
    }
}