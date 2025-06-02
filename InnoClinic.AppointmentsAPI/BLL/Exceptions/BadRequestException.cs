using System.Diagnostics.CodeAnalysis;

namespace BLL.Exceptions;

[ExcludeFromCodeCoverage]
public class BadRequestException<T>(string message) : AppointmentException($"Invalid {typeof(T).Name} data: {message}.")
{ }