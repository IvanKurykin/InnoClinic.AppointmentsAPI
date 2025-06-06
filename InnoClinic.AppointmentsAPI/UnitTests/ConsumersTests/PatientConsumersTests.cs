using AutoMapper;
using BLL.Consumers.PatientConsumers;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Patient;
using MassTransit;
using Moq;

namespace UnitTests.ConsumersTests;

public class PatientConsumersTests
{
    private readonly Mock<IPatientRepository> _patientRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public PatientConsumersTests()
    {
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task PatientCreatedConsumerShouldMapAndCallCreateAsync()
    {
        var consumer = new PatientCreatedConsumer(_patientRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new PatientCreatedIntegrationEvent { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "LastName", MiddleName = "MiddleName", PhoneNumber = "+2681635932" };
        var patientEntity = new Patient { Id = integrationEvent.Id, FirstName = "Jane", LastName = "LastName", MiddleName = "MiddleName", PhoneNumber = "+2681635932" };

        _mapperMock.Setup(m => m.Map<Patient>(integrationEvent)).Returns(patientEntity);

        var consumeContextMock = new Mock<ConsumeContext<PatientCreatedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Patient>(integrationEvent), Times.Once);
        _patientRepositoryMock.Verify(r => r.CreateAsync(patientEntity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task PatientUpdatedConsumerShouldMapAndCallUpdateAsync()
    {
        var consumer = new PatientUpdatedConsumer(_patientRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new PatientUpdatedIntegrationEvent { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "LastName", MiddleName = "MiddleName", PhoneNumber = "+2681635932" };
        var patientEntity = new Patient { Id = integrationEvent.Id, FirstName = "Jane", LastName = "LastName", MiddleName = "MiddleName", PhoneNumber = "+2681635932" };

        _mapperMock.Setup(m => m.Map<Patient>(integrationEvent)).Returns(patientEntity);

        var consumeContextMock = new Mock<ConsumeContext<PatientUpdatedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Patient>(integrationEvent), Times.Once);
        _patientRepositoryMock.Verify(r => r.UpdateAsync(patientEntity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task PatientDeletedConsumerShouldMapAndCallDeleteAsync()
    {
        var consumer = new PatientDeletedConsumer(_patientRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new PatientDeletedIntegrationEvent { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "LastName", MiddleName = "MiddleName", PhoneNumber = "+2681635932" };
        var patientEntity = new Patient { Id = integrationEvent.Id, FirstName = "Jane", LastName = "LastName", MiddleName = "MiddleName", PhoneNumber = "+2681635932" };

        _mapperMock.Setup(m => m.Map<Patient>(integrationEvent)).Returns(patientEntity);

        var consumeContextMock = new Mock<ConsumeContext<PatientDeletedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Patient>(integrationEvent), Times.Once);
        _patientRepositoryMock.Verify(r => r.DeleteAsync(patientEntity, It.IsAny<CancellationToken>()), Times.Once);
    }
}