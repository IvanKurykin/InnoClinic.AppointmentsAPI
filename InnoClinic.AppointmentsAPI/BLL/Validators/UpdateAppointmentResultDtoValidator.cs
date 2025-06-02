using BLL.Dto;
using FluentValidation;

namespace BLL.Validators;

public class UpdateAppointmentResultDtoValidator : AbstractValidator<UpdateAppointmentResultDto>
{
    public UpdateAppointmentResultDtoValidator()
    {
        Include(new AppointmentResultBaseDtoValidator());
    }
}