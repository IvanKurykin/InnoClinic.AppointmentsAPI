using AutoMapper;
using BLL.Dto;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class AppointmentResultService(IAppointmentResultRepository repository, IMapper mapper) : GenericService<AppointmentResult, AppointmentResultDto, CreateAppointmentResultDto, UpdateAppointmentResultDto>(repository, mapper), IAppointmentResultService
{
    public async Task<AppointmentResultDto?> GetResultWithAppointmentAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetByIdAsync(id, cancellationToken);

        if (result is null) throw new AppointmentResultNotFoundException(id);

        return _mapper.Map<AppointmentResultDto>(result);
    }
    public async Task<IReadOnlyCollection<AppointmentResultDto>> GetResultsByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetByDoctorIdAsync(doctorId, cancellationToken);

        return _mapper.Map<IReadOnlyCollection<AppointmentResultDto>>(result);
    }
}