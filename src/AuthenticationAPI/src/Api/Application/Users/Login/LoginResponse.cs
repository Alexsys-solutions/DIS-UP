
using AuthenticationAPI.Api.Application.Common.Models;

namespace AuthenticationAPI.Api.Application.Users.Login;

/// <summary>
/// Represents the response model for user login operations.
/// </summary>
public record LoginResponse
{
    /// <summary>
    /// Gets or sets the access token for the authenticated user.
    /// </summary>
    public string? AccessToken { get; init; }

    /// <summary>
    /// Gets or sets a boolean indicating whether the user is authenticated.
    /// </summary>
    public bool IsAutheticated { get; init; }

    /// <summary>
    /// Gets or sets a message describing the result of the login operation.
    /// </summary>
    public string Message { get; init; } = default!;

    /// <summary>
    /// Gets or sets the user information in the response.
    /// </summary>
    public UserResponse? User { get; init; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public IList<string>? Roles { get; set; } = [];
}
