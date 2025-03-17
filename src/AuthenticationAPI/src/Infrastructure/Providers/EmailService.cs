using AuthenticationAPI.Domain.Common.Interfaces;
using System.Net.Mail;
using System.Net;

namespace AuthenticationAPI.Infrastructure.Providers;
/// <summary>
/// Implementation of the <see cref="IEmailService"/> interface for sending emails.
/// </summary>
public class EmailService : IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="to">The recipient email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using (var client = new SmtpClient("smtp.gmail.com", 587))
        {
            client.Credentials = new NetworkCredential("b.yasser@alexsys.solutions", "Yasserder222***");
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress("b.yasser@alexsys.solutions"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
