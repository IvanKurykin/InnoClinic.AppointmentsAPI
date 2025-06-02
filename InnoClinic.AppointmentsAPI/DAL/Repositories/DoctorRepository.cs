using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories;

public class DoctorRepository(ApplicationDbContext context) : GenericRepository<Doctor>(context), IDoctorRepository
{ }