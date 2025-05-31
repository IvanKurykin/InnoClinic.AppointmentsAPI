using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories;

public class PatientRepository(ApplicationDbContext context) : GenericRepository<Patient>(context), IPatientRepository
{ }