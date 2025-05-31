namespace DAL.Entities;

public sealed class Patient : Person
{
    public string Email { get; set; } = string.Empty;
    public required string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}