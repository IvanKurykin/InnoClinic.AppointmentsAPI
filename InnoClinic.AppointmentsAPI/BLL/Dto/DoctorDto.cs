namespace BLL.Dto;

public sealed class DoctorDto : PersonDto
{
    public Guid Id { get; set; }
    public string Specialization { get; set; } = string.Empty;
}