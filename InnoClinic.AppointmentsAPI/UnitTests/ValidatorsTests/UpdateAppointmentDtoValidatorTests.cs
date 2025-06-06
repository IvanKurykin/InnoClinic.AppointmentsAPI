using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;
using UnitTests.TestCases;

namespace UnitTests.ValidatorsTests;

public class UpdateAppointmentDtoValidatorTests
{
    private readonly UpdateAppointmentDtoValidator _validator;

    public UpdateAppointmentDtoValidatorTests()
    {
        _validator = new UpdateAppointmentDtoValidator();
    }

    [Fact]
    public void ShouldInheritFromAppointmentBaseDtoValidator()
    {
        var model = new UpdateAppointmentDto
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
    public void ShouldNotHaveErrorWhenAllFieldsAreValid()
    {
        var model = new UpdateAppointmentDto
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