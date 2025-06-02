using AutoMapper;
using BLL.Dto;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class AppointmentService(IAppointmentRepository repository, IMapper mapper) : GenericService<Appointment, AppointmentDto, CreateAppointmentDto, UpdateAppointmentDto>(repository, mapper), IAppointmentService
{
    public async Task ApproveAppointmentAsync(Guid id, bool isApproved, CancellationToken cancellationToken = default)
    {
        var appointment = await repository.GetByIdWithDetailsAsync(id, cancellationToken);

        if (appointment is null) throw new AppointmentNotFoundException(id);
        
        appointment.IsAproved = isApproved;

        await _repository.UpdateAsync(appointment, cancellationToken);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdWithDependenciesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = await repository.GetByIdWithDetailsAsync(id, cancellationToken);

        if (appointment is null) throw new AppointmentNotFoundException(id);

        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<IReadOnlyCollection<AppointmentDto>> GetAppointmentsWithDependenciesAsync(CancellationToken cancellationToken = default)
    {
        var appointments = await repository.GetAllWithDetailsAsync(cancellationToken);

        return _mapper.Map<IReadOnlyCollection<AppointmentDto>>(appointments);
    }
}