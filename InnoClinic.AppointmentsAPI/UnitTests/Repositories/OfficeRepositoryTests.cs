using DAL.Context;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;

namespace UnitTests.Repositories;

public class OfficeRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly OfficeRepository _repository;

    public OfficeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new OfficeRepository(_context);
    }

    [Fact]
    public async Task CreateAsyncShouldAddOfficeToDatabase()
    {
        var office = AppointmentTestCases.TestOffice;

        var createdOffice = await _repository.CreateAsync(office);
        var officeInDb = await _context!.Offices!.FindAsync(office.Id);

        createdOffice.Should().BeEquivalentTo(office);
        officeInDb.Should().NotBeNull();
        officeInDb.Should().BeEquivalentTo(office);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateOfficeDataInDatabase()
    {
        var office = AppointmentTestCases.TestOffice;
        await _context!.Offices!.AddAsync(office);
        await _context.SaveChangesAsync();

        office.City = "NewCity";

        var updatedOffice = await _repository.UpdateAsync(office);
        var officeInDb = await _context.Offices.FindAsync(office.Id);

        updatedOffice.City.Should().Be("NewCity");
        officeInDb!.City.Should().Be("NewCity");
    }

    [Fact]
    public async Task DeleteAsyncShouldRemoveOfficeFromDatabase()
    {
        var office = AppointmentTestCases.TestOffice;
        await _context!.Offices!.AddAsync(office);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(office);
        var officeInDb = await _context.Offices.FindAsync(office.Id);

        officeInDb.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnCorrectOffice()
    {
        var office = AppointmentTestCases.TestOffice;
        await _context!.Offices!.AddAsync(office);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(office.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(office);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllOffices()
    {
        var offices = new List<DAL.Entities.Office>
        {
            AppointmentTestCases.TestOffice,
            new() { Id = Guid.NewGuid().ToString(), City = "OtherCity", Street = "OfficeStreet", HouseNumber = "10" }
        };
        await _context!.Offices!.AddRangeAsync(offices);
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