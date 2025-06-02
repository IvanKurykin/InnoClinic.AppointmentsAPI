using DAL.Entities;

namespace DAL.Interfaces;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<Appointment?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Appointment>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}