namespace AuthenticationAPI.Api.Application.Users.UnlockUser;

/// <summary>
/// Represents a query for unlocking a user.
/// </summary>
public class UnlockUserQuery : IRequest<Unit>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnlockUserQuery"/> class with the specified user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to unlock.</param>
    public UnlockUserQuery(string userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Gets or sets the ID of the user to unlock.
    /// </summary>
    public string UserId { get; set; }
}
