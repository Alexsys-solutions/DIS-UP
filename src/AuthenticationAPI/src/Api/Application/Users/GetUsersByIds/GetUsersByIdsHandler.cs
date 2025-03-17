using AuthenticationAPI.Api.Application.Common.Models;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Api.Application.Users.GetUsersByIds;

/// <summary>
/// Handler for the <see cref="GetUsersByIdsQuery"/> class.
/// </summary>
public class GetUsersByIdsHandler : IRequestHandler<GetUsersByIdsQuery, List<UserResponse>>
{
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersByIdsHandler"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    public GetUsersByIdsHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Handles the request to get users by IDs.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of user responses.</returns>
    public async Task<List<UserResponse>> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve all users with the provided Ids from the UserManager
        var users = await _userManager.Users.Where(p => request.Ids.Contains(p.Id)).ToListAsync(cancellationToken);
        // Adapt the User entities to UserResponse models
        var response = users.Adapt<List<UserResponse>>();

        return response;
    }
}
