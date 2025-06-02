namespace BLL.Dto;

public sealed class PatientDto : PersonDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public required string PhoneNumber { get; set; }
}