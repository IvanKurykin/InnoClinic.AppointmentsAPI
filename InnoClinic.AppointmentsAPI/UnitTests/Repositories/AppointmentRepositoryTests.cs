using DAL.Context;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;

namespace UnitTests.Repositories;

public class AppointmentRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly AppointmentRepository _repository;

    public AppointmentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new AppointmentRepository(_context);
    }

    [Fact]
    public async Task GetByIdWithDetailsAsyncShouldReturnAppointmentWithAllIncludes()
    {
        var appointment = AppointmentTestCases.ValidAppointment;
        await _context!.Appointments!.AddAsync(appointment);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdWithDetailsAsync(appointment.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(appointment.Id);
        result.Doctor.Should().NotBeNull();
        result.Patient.Should().NotBeNull();
        result.Service.Should().NotBeNull();
        result.Office.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllWithDetailsAsyncShouldReturnAllAppointmentsWithAllIncludes()
    {
        var appointments = new List<Appointment>
        {
            AppointmentTestCases.ValidAppointment,
            new() { Id = Guid.NewGuid(), OfficeId = Guid.NewGuid().ToString(), Date = DateTime.UtcNow, Doctor = new() { FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName" }, Patient = new() { PhoneNumber = "1", FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName"  }, Office = new() { City = "City", Street = "Street", HouseNumber = "10", Id = Guid.NewGuid().ToString()}, Service = new() { Name = "Name" } }
        };
        await _context!.Appointments!.AddRangeAsync(appointments);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllWithDetailsAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.All(a => a.Doctor != null && a.Patient != null && a.Service != null && a.Office != null).Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsyncShouldAddAppointmentToDatabase()
    {
        var appointment = AppointmentTestCases.ValidAppointment;

        var createdAppointment = await _repository.CreateAsync(appointment);
        var appointmentInDb = await _context!.Appointments!.FindAsync(appointment.Id);

        createdAppointment.Should().BeEquivalentTo(appointment);
        appointmentInDb.Should().NotBeNull();
        appointmentInDb!.Id.Should().Be(appointment.Id);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateAppointmentDataInDatabase()
    {
        var appointment = AppointmentTestCases.ValidAppointment;
        await _context!.Appointments!.AddAsync(appointment);
        await _context.SaveChangesAsync();

        appointment.IsAproved = true;
        
        var updatedAppointment = await _repository.UpdateAsync(appointment);
        var appointmentInDb = await _context.Appointments.FindAsync(appointment.Id);

        updatedAppointment.IsAproved.Should().BeTrue();
        appointmentInDb!.IsAproved.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsyncShouldRemoveAppointmentFromDatabase()
    {
        var appointment = AppointmentTestCases.ValidAppointment;
        await _context!.Appointments!.AddAsync(appointment);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(appointment);
        var appointmentInDb = await _context.Appointments.FindAsync(appointment.Id);

        appointmentInDb.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnAppointmentWithoutIncludes()
    {
        var appointment = AppointmentTestCases.ValidAppointment;
        await _context!.Appointments!.AddAsync(appointment);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(appointment.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(appointment.Id);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllAppointments()
    {
        var appointments = new List<Appointment>
        {
            AppointmentTestCases.ValidAppointment,
            new() { Id = Guid.NewGuid(), Date = DateTime.UtcNow, OfficeId = Guid.NewGuid().ToString(), Doctor = new() { FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName" }, Patient = new() { PhoneNumber = "1", FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName"  }, Office = new() { City = "City", Street = "Street", HouseNumber = "10", Id = Guid.NewGuid().ToString()}, Service = new() { Name = "Name" } }
        };
        await _context!.Appointments!.AddRangeAsync(appointments);
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