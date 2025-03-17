using AuthenticationAPI.Api.Application.Common.Exceptions;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.AspNetCore.Identity;


namespace AuthenticationAPI.Api.Application.Users.UnlockUser;

/// <summary>
/// Handler for unlocking a user.
/// </summary>
public class UnlockUserHandler : IRequestHandler<UnlockUserQuery, Unit>
{
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnlockUserHandler"/> class.
    /// </summary>
    /// <param name="userManager">The user manager to manage users.</param>
    public UnlockUserHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Handles the request to unlock a user.
    /// </summary>
    /// <param name="request">The request object containing the user ID to unlock.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<Unit> Handle(UnlockUserQuery request, CancellationToken cancellationToken)
    {
        // Find the user you want to unlock (you need to provide the user's id)
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
            throw new NotFoundException("User Not Found!");

        // Unlock the user
        await _userManager.SetLockoutEndDateAsync(user, null);

        // Return success
        return Unit.Value;
    }
}
