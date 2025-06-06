using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorsTests;

public class AppointmentResultBaseDtoValidatorTests
{
    private readonly AppointmentResultBaseDtoValidator _validator;

    public AppointmentResultBaseDtoValidatorTests()
    {
        _validator = new AppointmentResultBaseDtoValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenAppointmentIdIsEmpty()
    {
        var model = new AppointmentResultBaseDto
        {
            AppointmentId = Guid.Empty,
            DoctorId = Guid.NewGuid(),
            DateOfTheResult = DateTime.UtcNow,
            FullNameOfThePatient = "John Doe",
            PatientsDateOfBirth = DateTime.UtcNow.AddYears(-30),
            FullNameOfTheDoctor = "Dr. Smith",
            ServiceName = "Checkup",
            Complaints = "Headache",
            Conclusions = "Migraine",
            Recommendations = "Rest"
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(ar => ar.AppointmentId).WithErrorMessage("Appointment ID must not be empty.");
    }

    [Fact]
    public void ShouldHaveErrorWhenDateOfTheResultIsInFuture()
    {
        var model = new AppointmentResultBaseDto
        {
            AppointmentId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            DateOfTheResult = DateTime.UtcNow.AddDays(1),
            FullNameOfThePatient = "John Doe",
            PatientsDateOfBirth = DateTime.UtcNow.AddYears(-30),
            FullNameOfTheDoctor = "Dr. Smith",
            ServiceName = "Checkup",
            Complaints = "Headache",
            Conclusions = "Migraine",
            Recommendations = "Rest"
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(ar => ar.DateOfTheResult).WithErrorMessage("Date of the result cannot be in the future.");
    }

    [Fact]
    public void ShouldHaveErrorWhenFullNameOfThePatientExceedsMaxLength()
    {
        var model = new AppointmentResultBaseDto
        {
            AppointmentId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            DateOfTheResult = DateTime.UtcNow,
            FullNameOfThePatient = new string('A', 301),
            PatientsDateOfBirth = DateTime.UtcNow.AddYears(-30),
            FullNameOfTheDoctor = "Dr. Smith",
            ServiceName = "Checkup",
            Complaints = "Headache",
            Conclusions = "Migraine",
            Recommendations = "Rest"
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(ar => ar.FullNameOfThePatient).WithErrorMessage("Patient's full name cannot be longer than 300 characters.");
    }

    [Fact]
    public void ShouldHaveErrorWhenPatientsDateOfBirthIsInFuture()
    {
        var model = new AppointmentResultBaseDto
        {
            AppointmentId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            DateOfTheResult = DateTime.UtcNow,
            FullNameOfThePatient = "John Doe",
            PatientsDateOfBirth = DateTime.UtcNow.AddDays(1),
            FullNameOfTheDoctor = "Dr. Smith",
            ServiceName = "Checkup",
            Complaints = "Headache",
            Conclusions = "Migraine",
            Recommendations = "Rest"
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(ar => ar.PatientsDateOfBirth).WithErrorMessage("Patient's date of birth must be in the past.");
    }
}