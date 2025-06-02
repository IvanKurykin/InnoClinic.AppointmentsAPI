namespace BLL.Dto;

public abstract class AppointmentResultBaseDto
{
    public Guid AppointmentId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime DateOfTheResult { get; set; }
    public string FullNameOfThePatient { get; set; } = string.Empty;
    public DateTime PatientsDateOfBirth { get; set; }
    public string FullNameOfTheDoctor { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string Complaints { get; set; } = string.Empty;
    public string Conclusions { get; set; } = string.Empty;
    public string Recommendations { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
}