namespace DAL.Entities;

public sealed class Patient 
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public string Email { get; set; } = string.Empty;
    public required string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
}