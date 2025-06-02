using System.Diagnostics.CodeAnalysis;
using DAL.Entities;

namespace BLL.Exceptions;

[ExcludeFromCodeCoverage]
public class AppointmentNotFoundException(Guid id) : NotFoundException<Appointment>(id)
{ }