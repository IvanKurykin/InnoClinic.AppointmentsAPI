using DAL.Context;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;

namespace UnitTests.Repositories;

public class ServiceRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ServiceRepository _repository;

    public ServiceRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ServiceRepository(_context);
    }

    [Fact]
    public async Task CreateAsyncShouldAddServiceToDatabase()
    {
        var service = AppointmentTestCases.TestService;

        var createdService = await _repository.CreateAsync(service);
        var serviceInDb = await _context!.Services!.FindAsync(service.Id);

        createdService.Should().BeEquivalentTo(service);
        serviceInDb.Should().NotBeNull();
        serviceInDb.Should().BeEquivalentTo(service);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateServiceDataInDatabase()
    {
        var service = AppointmentTestCases.TestService;
        await _context!.Services!.AddAsync(service);
        await _context.SaveChangesAsync();

        service.Name = "UpdatedServiceName";

        var updatedService = await _repository.UpdateAsync(service);
        var serviceInDb = await _context.Services.FindAsync(service.Id);

        updatedService.Name.Should().Be("UpdatedServiceName");
        serviceInDb!.Name.Should().Be("UpdatedServiceName");
    }

    [Fact]
    public async Task DeleteAsyncShouldRemoveServiceFromDatabase()
    {
        var service = AppointmentTestCases.TestService;
        await _context!.Services!.AddAsync(service);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(service);
        var serviceInDb = await _context.Services.FindAsync(service.Id);

        serviceInDb.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnCorrectService()
    {
        var service = AppointmentTestCases.TestService;
        await _context!.Services!.AddAsync(service);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(service.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(service);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllServices()
    {
        var services = new List<DAL.Entities.Service>
        {
            AppointmentTestCases.TestService,
            new() { Id = Guid.NewGuid(), Name = "AnotherService" }
        };
        await _context!.Services!.AddRangeAsync(services);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}