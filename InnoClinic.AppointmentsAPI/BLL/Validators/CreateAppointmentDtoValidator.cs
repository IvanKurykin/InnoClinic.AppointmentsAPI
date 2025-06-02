using BLL.Dto;
using FluentValidation;

namespace BLL.Validators;

public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
{
    public CreateAppointmentDtoValidator()
    {
        Include(new AppointmentBaseDtoValidator());

        RuleFor(a => a.CreatedBy)
            .MaximumLength(255).WithMessage("'Created By' field cannot be longer than 255 characters.");
    }
}