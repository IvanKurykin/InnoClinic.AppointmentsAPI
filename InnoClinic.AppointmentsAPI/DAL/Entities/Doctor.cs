namespace DAL.Entities;

public sealed class Doctor : Person
{
    public DateTime DateOfBirth { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public IEnumerable<AppointmentResult>? Results { get; set; }
}