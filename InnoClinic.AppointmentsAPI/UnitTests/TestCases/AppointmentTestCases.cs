using BLL.Dto;
using DAL.Entities;

namespace UnitTests.TestCases;

public static class AppointmentTestCases
{
    public static Doctor TestDoctor => new Doctor { Id = Guid.NewGuid(), FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName" };
    public static Patient TestPatient => new Patient { Id = Guid.NewGuid(), FirstName = "FirstName", LastName = "LastName", MiddleName = "MiddleName", PhoneNumber = "+3459143324" };
    public static Service TestService => new Service { Id = Guid.NewGuid(), Name = "ServiceName" };
    public static Office TestOffice => new Office { Id = Guid.NewGuid().ToString(), City = "City", HouseNumber = "10", Street = "Street" };

    public static Appointment ValidAppointment => new Appointment
    {
        Id = Guid.NewGuid(),
        Doctor = TestDoctor,
        Patient = TestPatient,
        Service = TestService,
        Office = TestOffice,
        OfficeId = TestOffice.Id,
        IsAproved = false,
        Date = DateTime.UtcNow.AddDays(1)
    };

    public static AppointmentDto ValidAppointmentDto => new AppointmentDto
    {
        Id = ValidAppointment.Id,
        PatientId = TestPatient.Id,
        DoctorId = TestDoctor.Id,
        ServiceId = TestService.Id,
        OfficeId = TestOffice.Id,
        Doctor = new DoctorDto { Id = TestDoctor.Id, FirstName = TestDoctor.FirstName, LastName = TestDoctor.LastName, MiddleName = TestDoctor.MiddleName },
        Patient = new PatientDto { Id = TestPatient.Id, FirstName = TestPatient.FirstName, LastName = TestPatient.LastName, MiddleName = TestPatient.MiddleName, PhoneNumber = TestPatient.PhoneNumber },
        Service = new ServiceDto { Id = TestService.Id, Name = TestService.Name },
        Office = new OfficeDto { Id = TestOffice.Id, City = TestOffice.City, HouseNumber = TestOffice.HouseNumber, Street = TestOffice.Street },
        IsAproved = false,
        Date = ValidAppointment.Date
    };

    public static CreateAppointmentDto ValidCreateAppointmentDto => new CreateAppointmentDto
    {
        DoctorId = TestDoctor.Id,
        PatientId = TestPatient.Id,
        ServiceId = TestService.Id,
        OfficeId = TestOffice.Id,
        Date = DateTime.UtcNow.AddDays(1)
    };

    public static UpdateAppointmentDto ValidUpdateAppointmentDto => new UpdateAppointmentDto
    {
        DoctorId = TestDoctor.Id,
        PatientId = TestPatient.Id,
        ServiceId = TestService.Id,
        OfficeId = TestOffice.Id,
        Date = DateTime.UtcNow.AddDays(2)
    };
}