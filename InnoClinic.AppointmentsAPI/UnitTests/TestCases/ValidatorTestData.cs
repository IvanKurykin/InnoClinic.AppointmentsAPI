namespace UnitTests.TestCases;

public static class ValidatorTestData
{
    public const string PatientIdRequired = "Patient ID must not be empty.";
    public const string DoctorIdRequired = "Doctor ID must not be empty.";
    public const string ServiceIdRequired = "Service ID must not be empty.";
    public const string OfficeIdRequired = "Office ID must not be empty.";
    public const string DateNotPast = "Appointment date cannot be in the past.";
    public const string TimeEndAfterStart = "End time must be after start time.";
    public const string CreatedByMaxLength = "'Created By' field cannot be longer than 255 characters.";
    public const string ValidCreatedBy = "test@example.com";

    private static readonly Guid _testId = Guid.NewGuid();
    private static readonly Guid _emptyId = Guid.Empty;
    private static readonly DateTime _validDate = DateTime.UtcNow.AddDays(1).Date;
    private static readonly DateTime _validTimeStart = DateTime.Today.AddHours(10);
    private static readonly DateTime _validTimeEnd = DateTime.Today.AddHours(11);

    public static Guid TestId => _testId;
    public static Guid EmptyId => _emptyId;
    public static DateTime ValidDate => _validDate;
    public static DateTime ValidTimeStart => _validTimeStart;
    public static DateTime ValidTimeEnd => _validTimeEnd;
}