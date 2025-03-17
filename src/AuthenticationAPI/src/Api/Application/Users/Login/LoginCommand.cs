namespace AuthenticationAPI.Api.Application.Users.Login;

/// <summary>
/// Represents a command for user login.
/// </summary>
public record LoginCommand : IRequest<LoginResponse>
{
    /// <summary>
    /// Gets or initializes the username for the login.
    /// </summary>
    public required string UserName { get; init; }

    /// <summary>
    /// Gets or initializes the password for the login.
    /// </summary>
    public required string Password { get; init; }
}
