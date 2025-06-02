using System.Diagnostics.CodeAnalysis;

namespace BLL.Exceptions;

[ExcludeFromCodeCoverage]
public class AppointmentException : Exception
{
    protected AppointmentException(string message) : base(message) { }
}