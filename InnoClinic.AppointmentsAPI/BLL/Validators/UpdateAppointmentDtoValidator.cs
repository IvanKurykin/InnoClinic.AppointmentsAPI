using BLL.Dto;
using FluentValidation;

namespace BLL.Validators;

public class UpdateAppointmentDtoValidator : AbstractValidator<UpdateAppointmentDto>
{
    public UpdateAppointmentDtoValidator()
    {
        Include(new AppointmentBaseDtoValidator());
    }
}