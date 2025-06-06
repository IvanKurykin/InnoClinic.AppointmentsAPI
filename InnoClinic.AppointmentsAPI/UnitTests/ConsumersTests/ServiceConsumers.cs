using AutoMapper;
using BLL.Consumers.ServiceConsumers;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Service;
using MassTransit;
using Moq;

namespace UnitTests.ConsumersTests;

public class ServiceConsumersTests
{
    private readonly Mock<IServiceRepository> _serviceRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public ServiceConsumersTests()
    {
        _serviceRepositoryMock = new Mock<IServiceRepository>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task ServiceCreatedConsumerShouldMapAndCallCreateAsync()
    {
        var consumer = new ServiceCreatedConsumer(_serviceRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new ServiceCreatedIntegrationEvent { Id = Guid.NewGuid(), Name = "Consultation" };
        var serviceEntity = new Service { Id = integrationEvent.Id, Name = "Consultation" };

        _mapperMock.Setup(m => m.Map<Service>(integrationEvent)).Returns(serviceEntity);

        var consumeContextMock = new Mock<ConsumeContext<ServiceCreatedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Service>(integrationEvent), Times.Once);
        _serviceRepositoryMock.Verify(r => r.CreateAsync(serviceEntity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ServiceUpdatedConsumerShouldMapAndCallUpdateAsync()
    {
        var consumer = new ServiceUpdatedConsumer(_serviceRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new ServiceUpdatedIntegrationEvent { Id = Guid.NewGuid(), Name = "Consultation" };
        var serviceEntity = new Service { Id = integrationEvent.Id, Name = "Consultation" };

        _mapperMock.Setup(m => m.Map<Service>(integrationEvent)).Returns(serviceEntity);

        var consumeContextMock = new Mock<ConsumeContext<ServiceUpdatedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Service>(integrationEvent), Times.Once);
        _serviceRepositoryMock.Verify(r => r.UpdateAsync(serviceEntity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ServiceDeletedConsumerShouldMapAndCallDeleteAsync()
    {
        var consumer = new ServiceDeletedConsumer(_serviceRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new ServiceDeletedIntegrationEvent { Id = Guid.NewGuid(), Name = "Consultation" };
        var serviceEntity = new Service { Id = integrationEvent.Id, Name = "Consultation" };

        _mapperMock.Setup(m => m.Map<Service>(integrationEvent)).Returns(serviceEntity);

        var consumeContextMock = new Mock<ConsumeContext<ServiceDeletedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Service>(integrationEvent), Times.Once);
        _serviceRepositoryMock.Verify(r => r.DeleteAsync(serviceEntity, It.IsAny<CancellationToken>()), Times.Once);
    }
}