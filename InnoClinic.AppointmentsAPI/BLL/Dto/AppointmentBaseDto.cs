namespace BLL.Dto;

public abstract class AppointmentBaseDto
{
    public required Guid PatientId { get; set; }
    public required Guid DoctorId { get; set; }
    public required Guid ServiceId { get; set; }
    public required string OfficeId { get; set; }
    public DateTime Date { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
}