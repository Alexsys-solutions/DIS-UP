using AuthenticationAPI.Api.Application.Common.Exceptions;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Api.Application.Users.LockoutUser;

/// <summary>
/// Handler for locking out a user.
/// </summary>
public class LockoutUserHandler : IRequestHandler<LockoutUserQuery, Unit>
{
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="LockoutUserHandler"/> class.
    /// </summary>
    /// <param name="userManager">The user manager to manage users.</param>
    public LockoutUserHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Handles the request to lock out a user.
    /// </summary>
    /// <param name="request">The request object containing the user ID to lockout.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<Unit> Handle(LockoutUserQuery request, CancellationToken cancellationToken)
    {
        // Find the user you want to lockout (you need to provide the user's id)
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
            throw new NotFoundException("User Not Found!");

        // Lock out the user
        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

        // Return success
        return Unit.Value;
    }
}