namespace AuthenticationAPI.Domain.Common.Interfaces;

/// <summary>
/// Interface for sending emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="to">The recipient email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendEmailAsync(string to, string subject, string body);
}
