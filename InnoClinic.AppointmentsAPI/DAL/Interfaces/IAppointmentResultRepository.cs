using DAL.Entities;

namespace DAL.Interfaces;

public interface IAppointmentResultRepository : IGenericRepository<AppointmentResult>
{
    Task<AppointmentResult?> GetWithAppointmentAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<AppointmentResult>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
}