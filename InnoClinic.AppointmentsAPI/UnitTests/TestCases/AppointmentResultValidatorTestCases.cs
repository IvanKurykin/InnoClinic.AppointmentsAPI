using BLL.Dto;

namespace UnitTests.TestCases;

public static class AppointmentResultValidatorTestCases
{
    public static AppointmentResultBaseDto ValidAppointmentResultBaseDto()
    {
        return new AppointmentResultBaseDto
        {
            AppointmentId = ValidatorTestData.TestId,
            DoctorId = ValidatorTestData.TestId,
            DateOfTheResult = DateTime.UtcNow,
            FullNameOfThePatient = "John Doe",
            PatientsDateOfBirth = DateTime.Now.AddYears(-30),
            FullNameOfTheDoctor = "Dr. Smith",
            ServiceName = "Consultation",
            Complaints = "Headache",
            Conclusions = "Migraine",
            Recommendations = "Rest",
            Diagnosis = "Migraine"
        };
    }

    public static AppointmentResultBaseDto AppointmentResultWithEmptyAppointmentId()
    {
        var dto = ValidAppointmentResultBaseDto();
        dto.AppointmentId = ValidatorTestData.EmptyId;
        return dto;
    }

    public static AppointmentResultBaseDto AppointmentResultWithFutureDate()
    {
        var dto = ValidAppointmentResultBaseDto();
        dto.DateOfTheResult = DateTime.UtcNow.AddDays(1);
        return dto;
    }

    public static AppointmentResultBaseDto AppointmentResultWithLongDiagnosis()
    {
        var dto = ValidAppointmentResultBaseDto();
        dto.Diagnosis = new string('a', 1001);
        return dto;
    }
}