using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories;

public class ServiceRepository(ApplicationDbContext context) : GenericRepository<Service>(context), IServiceRepository 
{ }