namespace BLL.Dto;

public sealed class DoctorDto : PersonDto
{
    public string Specialization { get; set; } = string.Empty;
}