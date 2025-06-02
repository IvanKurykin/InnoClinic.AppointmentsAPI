namespace DAL.Entities;

public abstract class Person
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; } = [];
}