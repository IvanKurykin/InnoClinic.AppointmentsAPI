namespace DAL.Entities;

public sealed class Service
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; } = [];
}