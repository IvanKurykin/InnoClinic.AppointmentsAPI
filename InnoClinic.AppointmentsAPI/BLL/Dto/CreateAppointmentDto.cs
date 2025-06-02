namespace BLL.Dto;

public sealed class CreateAppointmentDto : AppointmentBaseDto
{
    public string CreatedBy { get; set; } = string.Empty;
}