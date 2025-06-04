using BLL.Dto;

namespace BLL.Interfaces;

public interface IAppointmentService : IGenericService<AppointmentDto, CreateAppointmentDto, UpdateAppointmentDto>
{
    Task ApproveAppointmentAsync(Guid id, bool isApproved, CancellationToken cancellationToken = default);
    Task<AppointmentDto?> GetAppointmentByIdWithDependenciesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<AppointmentDto>> GetAppointmentsWithDependenciesAsync(CancellationToken cancellationToken = default);
}