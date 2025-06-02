using System.Diagnostics.CodeAnalysis;

namespace BLL.Exceptions;

[ExcludeFromCodeCoverage]
public class NotFoundException<T> : AppointmentException
{
    public NotFoundException(Guid id) : base($"{typeof(T).Name} with id {id} was not found.") { }

    public NotFoundException(string message) : base(message) { }
}