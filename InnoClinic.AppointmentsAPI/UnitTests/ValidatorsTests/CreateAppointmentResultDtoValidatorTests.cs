using UnitTests.TestCases;
using BLL.Dto;
using FluentValidation.TestHelper;
using BLL.Validators;

namespace UnitTests.ValidatorsTests;

public class CreateAppointmentResultDtoValidatorTests
{
    private readonly CreateAppointmentResultDtoValidator _validator;

    public CreateAppointmentResultDtoValidatorTests()
    {
        _validator = new CreateAppointmentResultDtoValidator();
    }

    [Fact]
    public void ShouldInheritFromAppointmentResultBaseDtoValidator()
    {
        var model = new CreateAppointmentResultDto
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
}