namespace AuthenticationAPI.Api.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when there are too many attempts.
/// </summary>
public class TooManyAttemptsException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TooManyAttemptsException"/> class.
    /// </summary>
    public TooManyAttemptsException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TooManyAttemptsException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public TooManyAttemptsException(string message)
        : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TooManyAttemptsException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public TooManyAttemptsException(string message, Exception inner)
        : base(message, inner) { }
}
