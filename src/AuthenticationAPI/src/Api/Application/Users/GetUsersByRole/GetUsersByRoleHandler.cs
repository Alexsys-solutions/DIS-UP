using AuthenticationAPI.Api.Application.Common.Models;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;

namespace AuthenticationAPI.Api.Application.Users.GetUsersByRole;


/// <summary>
/// Handler for retrieving users by role.
/// </summary>
public class GetUsersByRoleHandler : IRequestHandler<GetUsersByRoleQuery, List<UserResponse>>
{
    private readonly IUserRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersByRoleHandler"/> class.
    /// </summary>
    /// <param name="repository">The user repository.</param>
    public GetUsersByRoleHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the request to retrieve users by role.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of users with the specified role.</returns>
    public async Task<List<UserResponse>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetUsersByRole(request.Role, request.AssignedBchId);

        // Adapt the User entities to UserResponse models
        var response = users.Adapt<IEnumerable<UserResponse>>().ToList();

        return response;
    }
}

