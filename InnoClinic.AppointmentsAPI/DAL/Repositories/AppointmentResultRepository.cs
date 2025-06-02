using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class AppointmentResultRepository(ApplicationDbContext context) : GenericRepository<AppointmentResult>(context), IAppointmentResultRepository
{
    public async Task<AppointmentResult?> GetWithAppointmentAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbSet.Include(ar => ar.Appointment).Include(ar => ar.Doctor).FirstOrDefaultAsync(ar => ar.Id == id, cancellationToken);

    public async Task<IReadOnlyCollection<AppointmentResult>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default) =>
        await _dbSet.Where(ar => ar.Doctor != null && ar.Doctor.Id == doctorId).Include(ar => ar.Appointment).ToListAsync(cancellationToken);
}