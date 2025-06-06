using DAL.Context;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;

namespace UnitTests.Repositories;

public class PatientRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly PatientRepository _repository;

    public PatientRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new PatientRepository(_context);
    }

    [Fact]
    public async Task CreateAsyncShouldAddPatientToDatabase()
    {
        var patient = AppointmentTestCases.TestPatient;

        var createdPatient = await _repository.CreateAsync(patient);
        var patientInDb = await _context!.Patients!.FindAsync(patient.Id);

        createdPatient.Should().BeEquivalentTo(patient);
        patientInDb.Should().NotBeNull();
        patientInDb.Should().BeEquivalentTo(patient);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdatePatientDataInDatabase()
    {
        var patient = AppointmentTestCases.TestPatient;
        await _context!.Patients!.AddAsync(patient);
        await _context.SaveChangesAsync();

        patient.FirstName = "UpdatedPatientName";

        var updatedPatient = await _repository.UpdateAsync(patient);
        var patientInDb = await _context.Patients.FindAsync(patient.Id);

        updatedPatient.FirstName.Should().Be("UpdatedPatientName");
        patientInDb!.FirstName.Should().Be("UpdatedPatientName");
    }

    [Fact]
    public async Task DeleteAsyncShouldRemovePatientFromDatabase()
    {
        var patient = AppointmentTestCases.TestPatient;
        await _context!.Patients!.AddAsync(patient);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(patient);
        var patientInDb = await _context.Patients.FindAsync(patient.Id);

        patientInDb.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnCorrectPatient()
    {
        var patient = AppointmentTestCases.TestPatient;
        await _context!.Patients!.AddAsync(patient);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(patient.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(patient);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllPatients()
    {
        var patients = new List<DAL.Entities.Patient>
        {
            AppointmentTestCases.TestPatient,
            new() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Smith", MiddleName = "X", PhoneNumber = "+123456789" }
        };
        await _context!.Patients!.AddRangeAsync(patients);
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