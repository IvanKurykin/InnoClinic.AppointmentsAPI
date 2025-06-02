namespace DAL.Entities;

public sealed class Appointment
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    public required Patient Patient { get; set; }

    public Guid DoctorId { get; set; }
    public required Doctor Doctor { get; set; }

    public Guid ServiceId { get; set; }
    public required Service Service { get; set; }

    public required string OfficeId { get; set; }
    public required Office Office { get; set; }

    public AppointmentResult? Result { get; set; }

    public DateTime Date { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = string.Empty;
    public bool IsAproved { get; set; } = false;
    public bool IsCanceled { get; set; } = false;
}