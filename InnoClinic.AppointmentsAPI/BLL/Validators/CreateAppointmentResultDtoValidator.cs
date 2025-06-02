using BLL.Dto;
using FluentValidation;

namespace BLL.Validators;

public class CreateAppointmentResultDtoValidator : AbstractValidator<CreateAppointmentResultDto>
{
    public CreateAppointmentResultDtoValidator()
    {
        Include(new AppointmentResultBaseDtoValidator());
    }
}