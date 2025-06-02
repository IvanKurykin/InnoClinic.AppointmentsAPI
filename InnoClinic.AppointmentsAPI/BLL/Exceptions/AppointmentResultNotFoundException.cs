using System.Diagnostics.CodeAnalysis;
using DAL.Entities;

namespace BLL.Exceptions;

[ExcludeFromCodeCoverage]
public class AppointmentResultNotFoundException(Guid id) : NotFoundException<AppointmentResult>(id)
{ }