using DAL.Context;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;

namespace UnitTests.Repositories;

public class DoctorRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly DoctorRepository _repository;

    public DoctorRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new DoctorRepository(_context);
    }

    [Fact]
    public async Task CreateAsyncShouldAddDoctorToDatabase()
    {
        var doctor = AppointmentTestCases.TestDoctor;

        var createdDoctor = await _repository.CreateAsync(doctor);
        var doctorInDb = await _context!.Doctors!.FindAsync(doctor.Id);

        createdDoctor.Should().BeEquivalentTo(doctor);
        doctorInDb.Should().NotBeNull();
        doctorInDb.Should().BeEquivalentTo(doctor);
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateDoctorDataInDatabase()
    {
        var doctor = AppointmentTestCases.TestDoctor;
        await _context!.Doctors!.AddAsync(doctor);
        await _context.SaveChangesAsync();

        doctor.FirstName = "UpdatedFirstName";

        var updatedDoctor = await _repository.UpdateAsync(doctor);
        var doctorInDb = await _context.Doctors.FindAsync(doctor.Id);

        updatedDoctor.FirstName.Should().Be("UpdatedFirstName");
        doctorInDb!.FirstName.Should().Be("UpdatedFirstName");
    }

    [Fact]
    public async Task DeleteAsyncShouldRemoveDoctorFromDatabase()
    {
        var doctor = AppointmentTestCases.TestDoctor;
        await _context!.Doctors!.AddAsync(doctor);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(doctor);
        var doctorInDb = await _context.Doctors.FindAsync(doctor.Id);

        doctorInDb.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnCorrectDoctor()
    {
        var doctor = AppointmentTestCases.TestDoctor;
        await _context!.Doctors!.AddAsync(doctor);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(doctor.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(doctor);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllDoctors()
    {
        var doctors = new List<DAL.Entities.Doctor>
        {
            AppointmentTestCases.TestDoctor,
            new() { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", MiddleName = "M" }
        };
        await _context!.Doctors!.AddRangeAsync(doctors);
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