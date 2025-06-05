namespace DAL.Entities;

public sealed class Office
{
    public required string Id { get; set; }
    public required string City { get; set; } 
    public required string Street { get; set; }
    public required string HouseNumber { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
}