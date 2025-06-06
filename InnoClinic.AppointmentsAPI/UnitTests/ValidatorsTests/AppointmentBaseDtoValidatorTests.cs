using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;
using UnitTests.TestCases;

namespace UnitTests.ValidatorsTests;

public class AppointmentBaseDtoValidatorTests
{
    private readonly AppointmentBaseDtoValidator _validator;

    public AppointmentBaseDtoValidatorTests()
    {
        _validator = new AppointmentBaseDtoValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenPatientIdIsEmpty()
    {
        var model = new AppointmentBaseDto
        {
            PatientId = Guid.Empty,
            DoctorId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            OfficeId = "1",
            Date = DateTime.UtcNow.AddDays(1),
            TimeStart = DateTime.UtcNow.AddHours(1),
            TimeEnd = DateTime.UtcNow.AddHours(2)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(a => a.PatientId).WithErrorMessage("Patient ID must not be empty.");
    }

    [Fact]
    public void ShouldHaveErrorWhenDoctorIdIsEmpty()
    {
        var model = new AppointmentBaseDto
        {
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.Empty,
            ServiceId = Guid.NewGuid(),
            OfficeId = "1",
            Date = DateTime.UtcNow.AddDays(1),
            TimeStart = DateTime.UtcNow.AddHours(1),
            TimeEnd = DateTime.UtcNow.AddHours(2)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(a => a.DoctorId).WithErrorMessage("Doctor ID must not be empty.");
    }

    [Fact]
    public void ShouldHaveErrorWhenServiceIdIsEmpty()
    {
        var model = new AppointmentBaseDto
        {
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            ServiceId = Guid.Empty,
            OfficeId = "1",
            Date = DateTime.UtcNow.AddDays(1),
            TimeStart = DateTime.UtcNow.AddHours(1),
            TimeEnd = DateTime.UtcNow.AddHours(2)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(a => a.ServiceId).WithErrorMessage("Service ID must not be empty.");
    }

    [Fact]
    public void ShouldHaveErrorWhenOfficeIdIsEmpty()
    {
        var model = new AppointmentBaseDto
        {
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            OfficeId = string.Empty,
            Date = DateTime.UtcNow.AddDays(1),
            TimeStart = DateTime.UtcNow.AddHours(1),
            TimeEnd = DateTime.UtcNow.AddHours(2)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(a => a.OfficeId).WithErrorMessage("Office ID must not be empty.");
    }

    [Fact]
    public void ShouldHaveErrorWhenDateIsInPast()
    {
        var model = new AppointmentBaseDto
        {
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            OfficeId = "1",
            Date = DateTime.UtcNow.AddDays(-1),
            TimeStart = DateTime.UtcNow.AddHours(1),
            TimeEnd = DateTime.UtcNow.AddHours(2)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(a => a.Date).WithErrorMessage("Appointment date cannot be in the past.");
    }

    [Fact]
    public void ShouldHaveErrorWhenTimeEndIsBeforeTimeStart()
    {
        var model = new AppointmentBaseDto
        {
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            OfficeId = "1",
            Date = DateTime.UtcNow.AddDays(1),
            TimeStart = DateTime.UtcNow.AddHours(2),
            TimeEnd = DateTime.UtcNow.AddHours(1)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(a => a.TimeEnd).WithErrorMessage("End time must be after start time.");
    }

    [Fact]
    public void ShouldNotHaveErrorWhenAllFieldsAre_alid()
    {
        var model = new AppointmentBaseDto
        {
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            OfficeId = "1",
            Date = DateTime.UtcNow.AddDays(1),
            TimeStart = DateTime.UtcNow.AddHours(1),
            TimeEnd = DateTime.UtcNow.AddHours(2)
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}