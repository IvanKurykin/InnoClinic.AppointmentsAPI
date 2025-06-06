using BLL.Dto;

namespace UnitTests.TestCases;

public class AppointmentValidatorTestCases
{
    public static CreateAppointmentDto ValidCreateAppointmentDto()
    {
        return new CreateAppointmentDto
        {
            PatientId = ValidatorTestData.TestId,
            DoctorId = ValidatorTestData.TestId,
            ServiceId = ValidatorTestData.TestId,
            OfficeId = ValidatorTestData.TestId.ToString(),
            Date = ValidatorTestData.ValidDate,
            TimeStart = ValidatorTestData.ValidTimeStart,
            TimeEnd = ValidatorTestData.ValidTimeEnd,
            CreatedBy = ValidatorTestData.ValidCreatedBy
        };
    }

    public static UpdateAppointmentDto ValidUpdateAppointmentDto()
    {
        return new UpdateAppointmentDto
        {
            PatientId = ValidatorTestData.TestId,
            DoctorId = ValidatorTestData.TestId,
            ServiceId = ValidatorTestData.TestId,
            OfficeId = ValidatorTestData.TestId.ToString(),
            Date = ValidatorTestData.ValidDate,
            TimeStart = ValidatorTestData.ValidTimeStart,
            TimeEnd = ValidatorTestData.ValidTimeEnd
        };
    }

    public static CreateAppointmentDto CreateAppointmentDtoWithEmptyPatientId()
    {
        var dto = ValidCreateAppointmentDto();
        dto.PatientId = ValidatorTestData.EmptyId;
        return dto;
    }

    public static CreateAppointmentDto CreateAppointmentDtoWithPastDate()
    {
        var dto = ValidCreateAppointmentDto();
        dto.Date = DateTime.UtcNow.AddDays(-1);
        return dto;
    }

    public static CreateAppointmentDto CreateAppointmentDtoWithInvalidTimeRange()
    {
        var dto = ValidCreateAppointmentDto();
        dto.TimeStart = new DateTime(12, 0, 0);
        dto.TimeEnd = new DateTime(11, 0, 0);
        return dto;
    }

    public static CreateAppointmentDto CreateAppointmentDtoWithLongCreatedBy()
    {
        var dto = ValidCreateAppointmentDto();
        dto.CreatedBy = new string('a', 256);
        return dto;
    }
}