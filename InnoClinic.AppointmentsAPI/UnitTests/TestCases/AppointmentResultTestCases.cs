using BLL.Dto;
using DAL.Entities;

namespace UnitTests.TestCases;

public static class AppointmentResultTestCases
{
    public static AppointmentResult ValidAppointmentResult => new AppointmentResult
    {
        Id = Guid.NewGuid(),
        Appointment = AppointmentTestCases.ValidAppointment,
        Doctor = AppointmentTestCases.TestDoctor,
        Diagnosis = "Test Diagnosis",
        Recommendations = "Test Recommendations",
        AppointmentId = Guid.NewGuid()
    };

    public static AppointmentResultDto ValidAppointmentResultDto => new AppointmentResultDto
    {
        Id = ValidAppointmentResult.Id,
        Diagnosis = ValidAppointmentResult.Diagnosis,
        Recommendations = ValidAppointmentResult.Recommendations
    };

    public static CreateAppointmentResultDto ValidCreateAppointmentResultDto => new CreateAppointmentResultDto
    {
        Diagnosis = "Test Diagnosis",
        Recommendations = "Test Recommendations",
        AppointmentId = Guid.NewGuid()
    };

    public static UpdateAppointmentResultDto ValidUpdateAppointmentResultDto => new UpdateAppointmentResultDto
    {
        Diagnosis = "Updated Diagnosis",
        Recommendations = "Updated Recommendations"
    };
}