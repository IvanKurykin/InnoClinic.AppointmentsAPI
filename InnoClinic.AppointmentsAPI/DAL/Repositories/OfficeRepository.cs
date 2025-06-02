using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories;

public class OfficeRepository(ApplicationDbContext context) : GenericRepository<Office>(context), IOfficeRepository
{ }