namespace AuthenticationAPI.Api.Application.Users.ConfirmEmail;

/// <summary>
/// Command for confirming a user's email.
/// </summary>
public class ConfirmEmailCommand : IRequest<string>
{
    public string UserId { get; set; }
    public string Token { get; set; }
}
