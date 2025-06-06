using DAL.Context;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;

namespace UnitTests.Repositories;

public class AppointmentResultRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly AppointmentResultRepository _repository;

    public AppointmentResultRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new AppointmentResultRepository(_context);
    }

    [Fact]
    public async Task GetWithAppointmentAsyncShouldReturnResultWithIncludes()
    {
        var appointmentResult = AppointmentResultTestCases.ValidAppointmentResult;
        await _context!.AppointmentResults!.AddAsync(appointmentResult);
        await _context.SaveChangesAsync();

        var result = await _repository.GetWithAppointmentAsync(appointmentResult.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(appointmentResult.Id);
        result.Appointment.Should().NotBeNull();
        result.Doctor.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByDoctorIdAsyncShouldReturnOnlyResultsForGivenDoctor()
    {
        var doctor1 = AppointmentTestCases.TestDoctor;
        var doctor2 = new Doctor { Id = Guid.NewGuid(), FirstName = "Other", LastName = "Doc", MiddleName = "T" };

        var resultForDoc1 = AppointmentResultTestCases.ValidAppointmentResult;
        resultForDoc1.Doctor = doctor1;

        var resultForDoc2 = new AppointmentResult { Id = Guid.NewGuid(), Doctor = doctor2, Appointment = new() { Date = DateTime.UtcNow, OfficeId = Guid.NewGuid().ToString(), Doctor = new() { FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName" }, Patient = new() { PhoneNumber = "1", FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName" }, Office = new() { City = "City", Street = "Street", HouseNumber = "10", Id = Guid.NewGuid().ToString() }, Service = new() { Name = "Name" } } };

        await _context!.AppointmentResults!.AddRangeAsync(resultForDoc1, resultForDoc2);
        await _context.SaveChangesAsync();

        var results = await _repository.GetByDoctorIdAsync(doctor1.Id);

        results.Should().NotBeNull();
        results.Should().HaveCount(1);
        results.First().Doctor!.Id.Should().Be(doctor1.Id);
    }

    [Fact]
    public async Task CreateAsyncShouldAddAppointmentResultToDatabase()
    {
        var appResult = AppointmentResultTestCases.ValidAppointmentResult;

        var createdResult = await _repository.CreateAsync(appResult);
        var resultInDb = await _context!.AppointmentResults!.FindAsync(appResult.Id);

        createdResult.Should().BeEquivalentTo(appResult);
        resultInDb.Should().NotBeNull();
        resultInDb!.Id.Should().Be(appResult.Id);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateAppointmentResultDataInDatabase()
    {
        var appResult = AppointmentResultTestCases.ValidAppointmentResult;
        await _context!.AppointmentResults!.AddAsync(appResult);
        await _context.SaveChangesAsync();

        appResult.Recommendations = "Updated Recommendations";

        var updatedResult = await _repository.UpdateAsync(appResult);
        var resultInDb = await _context.AppointmentResults.FindAsync(appResult.Id);

        updatedResult.Recommendations.Should().Be("Updated Recommendations");
        resultInDb!.Recommendations.Should().Be("Updated Recommendations");
    }

    [Fact]
    public async Task DeleteAsyncShouldRemoveAppointmentResultFromDatabase()
    {
        var appResult = AppointmentResultTestCases.ValidAppointmentResult;
        await _context!.AppointmentResults!.AddAsync(appResult);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(appResult);
        var resultInDb = await _context.AppointmentResults.FindAsync(appResult.Id);

        resultInDb.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnCorrectAppointmentResult()
    {
        var appResult = AppointmentResultTestCases.ValidAppointmentResult;
        await _context!.AppointmentResults!.AddAsync(appResult);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(appResult.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(appResult.Id);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllAppointmentResults()
    {
        var results = new List<AppointmentResult>
        {
            AppointmentResultTestCases.ValidAppointmentResult,
            new() { Id = Guid.NewGuid(), Diagnosis = "Another Diagnosis", Doctor = new() { FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName" }, Appointment = new() { Date = DateTime.UtcNow, OfficeId = Guid.NewGuid().ToString(), Doctor = new() { FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName" }, Patient = new() { PhoneNumber = "1", FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName" }, Office = new() { City = "City", Street = "Street", HouseNumber = "10", Id = Guid.NewGuid().ToString() }, Service = new() { Name = "Name" } } }
        };
        await _context!.AppointmentResults!.AddRangeAsync(results);
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