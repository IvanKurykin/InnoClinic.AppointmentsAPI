namespace UnitTests.TestCases;

public class ValidatorTestData
{
    public static Guid TestId = Guid.NewGuid();
    public static Guid EmptyId = Guid.Empty;

    public const string PatientIdRequired = "Patient ID must not be empty.";
    public const string DoctorIdRequired = "Doctor ID must not be empty.";
    public const string ServiceIdRequired = "Service ID must not be empty.";
    public const string OfficeIdRequired = "Office ID must not be empty.";
    public const string DateNotPast = "Appointment date cannot be in the past.";
    public const string TimeEndAfterStart = "End time must be after start time.";
    public const string CreatedByMaxLength = "'Created By' field cannot be longer than 255 characters.";

    public static DateTime ValidDate = DateTime.UtcNow.AddDays(1).Date;
    public static DateTime ValidTimeStart = new DateTime(10, 0, 0);
    public static DateTime ValidTimeEnd = new DateTime(11, 0, 0);
    public const string ValidCreatedBy = "test@example.com";
}