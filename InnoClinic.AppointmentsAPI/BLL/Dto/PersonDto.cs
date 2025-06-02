namespace BLL.Dto;

public class PersonDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
}