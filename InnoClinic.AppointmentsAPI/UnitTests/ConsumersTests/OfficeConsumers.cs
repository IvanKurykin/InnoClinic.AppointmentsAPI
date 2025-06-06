using AutoMapper;
using BLL.Consumers.OfficeConsumers;
using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Messaging.Contracts.Events.Office;
using MassTransit;
using Moq;

namespace UnitTests.ConsumersTests;

public class OfficeConsumersTests
{
    private readonly Mock<IOfficeRepository> _officeRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public OfficeConsumersTests()
    {
        _officeRepositoryMock = new Mock<IOfficeRepository>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task OfficeCreatedConsumerShouldMapAndCallCreateAsync()
    {
        var consumer = new OfficeCreatedConsumer(_officeRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new OfficeCreatedIntegrationEvent { Id = Guid.NewGuid().ToString(), City = "New York", Street = "Street", HouseNumber = "10" };
        var officeEntity = new Office { Id = integrationEvent.Id, City = "New York", Street = "Street", HouseNumber = "10" };

        _mapperMock.Setup(m => m.Map<Office>(integrationEvent)).Returns(officeEntity);

        var consumeContextMock = new Mock<ConsumeContext<OfficeCreatedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Office>(integrationEvent), Times.Once);
        _officeRepositoryMock.Verify(r => r.CreateAsync(officeEntity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OfficeUpdatedConsumerShouldMapAndCallUpdateAsync()
    {
        var consumer = new OfficeUpdatedConsumer(_officeRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new OfficeUpdatedIntegrationEvent { Id = Guid.NewGuid().ToString(), City = "New York", Street = "Street", HouseNumber = "10" };
        var officeEntity = new Office { Id = integrationEvent.Id, City = "New York", Street = "Street", HouseNumber = "10" };

        _mapperMock.Setup(m => m.Map<Office>(integrationEvent)).Returns(officeEntity);

        var consumeContextMock = new Mock<ConsumeContext<OfficeUpdatedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Office>(integrationEvent), Times.Once);
        _officeRepositoryMock.Verify(r => r.UpdateAsync(officeEntity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OfficeDeletedConsumerShouldMapAndCallDeleteAsync()
    {
        var consumer = new OfficeDeletedConsumer(_officeRepositoryMock.Object, _mapperMock.Object);

        var integrationEvent = new OfficeDeletedIntegrationEvent { Id = Guid.NewGuid().ToString(), City = "New York", Street = "Street", HouseNumber = "10" };
        var officeEntity = new Office { Id = integrationEvent.Id, City = "New York", Street = "Street", HouseNumber = "10" };

        _mapperMock.Setup(m => m.Map<Office>(integrationEvent)).Returns(officeEntity);

        var consumeContextMock = new Mock<ConsumeContext<OfficeDeletedIntegrationEvent>>();
        consumeContextMock.Setup(c => c.Message).Returns(integrationEvent);

        await consumer.Consume(consumeContextMock.Object);

        _mapperMock.Verify(m => m.Map<Office>(integrationEvent), Times.Once);
        _officeRepositoryMock.Verify(r => r.DeleteAsync(officeEntity, It.IsAny<CancellationToken>()), Times.Once);
    }
}