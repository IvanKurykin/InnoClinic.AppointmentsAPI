using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorsTests;

public class UpdateAppointmentResultDtoValidatorTests
{
    private readonly UpdateAppointmentResultDtoValidator _validator;

    public UpdateAppointmentResultDtoValidatorTests()
    {
        _validator = new UpdateAppointmentResultDtoValidator();
    }

    [Fact]
    public void ShouldInheritFromAppointmentResultBaseDtoValidator()
    {
        var model = new UpdateAppointmentResultDto
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