using AutoMapper;
using BLL.Consumers.DoctorConsumers;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Doctor;
using MassTransit;
using Moq;

namespace UnitTests.ConsumersTests;

public class DoctorConsumersTests
{
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public DoctorConsumersTests()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task DoctorCreatedConsumerShouldMapAndCallCreateAsync()
    {
        var consumer = new DoctorCreatedConsumer(_doctorRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new DoctorCreatedIntegrationEvent { Id = Guid.NewGuid(), FirstName = "John", LastName = "LastName", MiddleName = "MiddleName" };
        var doctorEntity = new Doctor { Id = integrationEvent.Id, FirstName = "John", LastName = "LastName", MiddleName = "MiddleName" };

        _mapperMock.Setup(m => m.Map<Doctor>(integrationEvent)).Returns(doctorEntity);

        var consumeContextMock = new Mock<ConsumeContext<DoctorCreatedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Doctor>(integrationEvent), Times.Once);
        _doctorRepositoryMock.Verify(r => r.CreateAsync(doctorEntity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DoctorUpdatedConsumerShouldMapAndCallUpdateAsync()
    {
        var consumer = new DoctorUpdatedConsumer(_doctorRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new DoctorUpdatedIntegrationEvent { Id = Guid.NewGuid(), FirstName = "John", LastName = "LastName", MiddleName = "MiddleName" };
        var doctorEntity = new Doctor { Id = integrationEvent.Id, FirstName = "John", LastName = "LastName", MiddleName = "MiddleName" };

        _mapperMock.Setup(m => m.Map<Doctor>(integrationEvent)).Returns(doctorEntity);

        var consumeContextMock = new Mock<ConsumeContext<DoctorUpdatedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Doctor>(integrationEvent), Times.Once);
        _doctorRepositoryMock.Verify(r => r.UpdateAsync(doctorEntity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DoctorDeletedConsumerShouldMapAndCallDeleteAsync()
    {
        var consumer = new DoctorDeletedConsumer(_doctorRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new DoctorDeletedIntegrationEvent { Id = Guid.NewGuid(), FirstName = "John", LastName = "LastName", MiddleName = "MiddleName" };
        var doctorEntity = new Doctor { Id = integrationEvent.Id, FirstName = "John", LastName = "LastName", MiddleName = "MiddleName" };

        _mapperMock.Setup(m => m.Map<Doctor>(integrationEvent)).Returns(doctorEntity);

        var consumeContextMock = new Mock<ConsumeContext<DoctorDeletedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Doctor>(integrationEvent), Times.Once);
        _doctorRepositoryMock.Verify(r => r.DeleteAsync(doctorEntity, It.IsAny<CancellationToken>()), Times.Once);
    }
}