using BLL.Dto;
using FluentValidation;

namespace BLL.Validators;

public class AppointmentBaseDtoValidator : AbstractValidator<AppointmentBaseDto>
{
    public AppointmentBaseDtoValidator()
    {
        RuleFor(a => a.PatientId)
            .NotEmpty().WithMessage("Patient ID must not be empty.");

        RuleFor(a => a.DoctorId)
            .NotEmpty().WithMessage("Doctor ID must not be empty.");

        RuleFor(a => a.ServiceId)
            .NotEmpty().WithMessage("Service ID must not be empty.");

        RuleFor(a => a.OfficeId)
            .NotEmpty().WithMessage("Office ID must not be empty.");

        RuleFor(a => a.Date)
            .NotEmpty().WithMessage("Appointment date must not be empty.")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Appointment date cannot be in the past.");

        RuleFor(a => a.TimeStart)
            .NotEmpty().WithMessage("Appointment start time must not be empty.");

        RuleFor(a => a.TimeEnd)
            .NotEmpty().WithMessage("Appointment end time must not be empty.")
            .GreaterThan(a => a.TimeStart).WithMessage("End time must be after start time.");
    }
}