using BLL.Dto;

namespace BLL.Interfaces;

public interface IAppointmentResultService : IGenericService<AppointmentResultDto, CreateAppointmentResultDto, UpdateAppointmentResultDto> 
{
    Task<AppointmentResultDto?> GetResultWithAppointmentAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<AppointmentResultDto>> GetResultsByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
}