using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class AppointmentRepository(ApplicationDbContext context) : GenericRepository<Appointment>(context), IAppointmentRepository
{
    public async Task<Appointment?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (_context.Appointments is null) return null;
        return await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Service).Include(a => a.Office).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Appointment>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        if (_context.Appointments is null) return new List<Appointment>();
        return await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Service).Include(a => a.Office).ToListAsync(cancellationToken);
    }
}