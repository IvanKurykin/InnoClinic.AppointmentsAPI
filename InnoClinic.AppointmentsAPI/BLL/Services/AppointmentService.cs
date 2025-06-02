using AutoMapper;
using BLL.Dto;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class AppointmentService(IAppointmentRepository repository, IMapper mapper) : GenericService<Appointment, AppointmentDto, CreateAppointmentDto, UpdateAppointmentDto>(repository, mapper), IAppointmentService
{
    public async Task<AppointmentDto?> GetAppointmentByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = await repository.GetByIdWithDetailsAsync(id, cancellationToken);

        if (appointment is null) throw new AppointmentNotFoundException(id);

        return mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<IReadOnlyCollection<AppointmentDto>> GetAppointmentsWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        var appointments = await repository.GetAllWithDetailsAsync(cancellationToken);

        return mapper.Map<IReadOnlyCollection<AppointmentDto>>(appointments);
    }
}