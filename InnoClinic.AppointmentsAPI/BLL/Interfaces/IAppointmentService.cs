using BLL.Dto;

namespace BLL.Interfaces;

public interface IAppointmentService : IGenericService<AppointmentDto, CreateAppointmentDto, UpdateAppointmentDto>
{
    Task ApproveAppointmentAsync(Guid id, bool isApproved, CancellationToken cancellationToken = default);
    Task<AppointmentDto?> GetAppointmentByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<AppointmentDto>> GetAppointmentsWithDetailsAsync(CancellationToken cancellationToken = default);
}