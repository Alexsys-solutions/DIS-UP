namespace AuthenticationAPI.Api.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested resource is not found.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    public NotFoundException() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public NotFoundException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with the name of the entity
    /// and the key information that identifies the entity not found.
    /// </summary>
    /// <param name="name">The name of the entity that was not found.</param>
    /// <param name="key">The key or identifier of the entity.</param>
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
