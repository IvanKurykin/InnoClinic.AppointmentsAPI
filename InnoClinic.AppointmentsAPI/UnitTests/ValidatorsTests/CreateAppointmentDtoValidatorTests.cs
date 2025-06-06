using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorsTests;

public class CreateAppointmentDtoValidatorTests
{
    private readonly CreateAppointmentDtoValidator _validator;

    public CreateAppointmentDtoValidatorTests()
    {
        _validator = new CreateAppointmentDtoValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenCreatedByExceedsMaxLength()
    {
        var model = new CreateAppointmentDto
        {
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            OfficeId = "1",
            Date = DateTime.UtcNow.AddDays(1),
            TimeStart = DateTime.UtcNow.AddHours(1),
            TimeEnd = DateTime.UtcNow.AddHours(2),
            CreatedBy = new string('A', 256)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(a => a.CreatedBy).WithErrorMessage("'Created By' field cannot be longer than 255 characters.");
    }

    [Fact]
    public void ShouldNotHaveErrorWhenAllFieldsAreValid()
    {
        var model = new CreateAppointmentDto
        {
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            OfficeId = "1",
            Date = DateTime.UtcNow.AddDays(1),
            TimeStart = DateTime.UtcNow.AddHours(1),
            TimeEnd = DateTime.UtcNow.AddHours(2),
            CreatedBy = "Admin"
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}