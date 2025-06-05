namespace DAL.Entities;

public sealed class Doctor 
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public ICollection<Appointment> Appointments { get; set; } = [];
    public ICollection<AppointmentResult>? Results { get; set; }
}