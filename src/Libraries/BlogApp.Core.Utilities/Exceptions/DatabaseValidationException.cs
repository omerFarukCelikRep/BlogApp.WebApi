using System.Globalization;
using System.Runtime.Serialization;

namespace BlogApp.Core.Utilities.Exceptions;
public class DatabaseValidationException : Exception
{
    public DatabaseValidationException() :base() { }

    public DatabaseValidationException(string? message) : base(message) { }

    public DatabaseValidationException(string? message, Exception? innerException) : base(message, innerException) { }

    public DatabaseValidationException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }

    protected DatabaseValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
