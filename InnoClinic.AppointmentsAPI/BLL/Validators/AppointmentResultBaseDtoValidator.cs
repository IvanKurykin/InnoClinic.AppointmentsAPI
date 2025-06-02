using BLL.Dto;
using FluentValidation;

namespace BLL.Validators;

public class AppointmentResultBaseDtoValidator : AbstractValidator<AppointmentResultBaseDto>
{
    public AppointmentResultBaseDtoValidator()
    {
        RuleFor(ar => ar.AppointmentId)
            .NotEmpty().WithMessage("Appointment ID must not be empty.");

        RuleFor(ar => ar.DoctorId)
            .NotEmpty().WithMessage("Doctor ID (who created the result) must not be empty.");

        RuleFor(ar => ar.DateOfTheResult)
            .NotEmpty().WithMessage("Date of the result must not be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date of the result cannot be in the future.");

        RuleFor(ar => ar.FullNameOfThePatient)
            .NotEmpty().WithMessage("Patient's full name must not be empty.")
            .MaximumLength(300).WithMessage("Patient's full name cannot be longer than 300 characters.");

        RuleFor(ar => ar.PatientsDateOfBirth)
            .NotEmpty().WithMessage("Patient's date of birth must not be empty.")
            .LessThan(DateTime.Now).WithMessage("Patient's date of birth must be in the past.");

        RuleFor(ar => ar.FullNameOfTheDoctor)
            .NotEmpty().WithMessage("Doctor's full name must not be empty.")
            .MaximumLength(300).WithMessage("Doctor's full name cannot be longer than 300 characters.");

        RuleFor(ar => ar.ServiceName)
            .NotEmpty().WithMessage("Service name must not be empty.")
            .MaximumLength(200).WithMessage("Service name cannot be longer than 200 characters.");

        RuleFor(ar => ar.Complaints)
            .NotEmpty().WithMessage("Complaints must not be empty.")
            .MaximumLength(2000).WithMessage("'Complaints' field cannot be longer than 2000 characters.");

        RuleFor(ar => ar.Conclusions)
            .NotEmpty().WithMessage("Conclusions must not be empty.")
            .MaximumLength(2000).WithMessage("'Conclusions' field cannot be longer than 2000 characters.");

        RuleFor(ar => ar.Recommendations)
            .NotEmpty().WithMessage("Recommendations must not be empty.")
            .MaximumLength(2000).WithMessage("'Recommendations' field cannot be longer than 2000 characters.");

        RuleFor(ar => ar.Diagnosis)
            .MaximumLength(1000).WithMessage("Diagnosis cannot be longer than 1000 characters.");
    }
}