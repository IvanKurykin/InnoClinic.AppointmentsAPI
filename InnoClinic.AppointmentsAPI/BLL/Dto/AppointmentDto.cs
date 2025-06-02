namespace BLL.Dto;

public class AppointmentDto : AppointmentBaseDto
{
    public Guid Id { get; set; }
    public required PatientDto Patient { get; set; }
    public required DoctorDto Doctor { get; set; }
    public required ServiceDto Service { get; set; }
    public required OfficeDto Office { get; set; }
    public AppointmentResultDto? Result { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public bool IsAproved { get; set; }
    public bool IsCanceled { get; set; }
}