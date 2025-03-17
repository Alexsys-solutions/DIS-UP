using Als.Foundation.Data.Abstractions.EntityFramework;
using AuthenticationAPI.Domain.ApplicationUser.Entities;

namespace AuthenticationAPI.Domain.ApplicationUser.Interfaces;

/// <summary>
/// Provides an interface for user-specific repository operations, 
/// extending the base repository functionalities for <see cref="User"/> entities.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Asynchronously retrieves a user by their username, including their roles.
    /// </summary>
    /// <param name="userName">The username of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user if found; otherwise, null.</returns>
    Task<User?> GetUserByName(string userName);

    /// <summary>
    /// Asynchronously retrieves a list of users based on their role and assigned bch ID by calling a stored procedure.
    /// </summary>
    /// <param name="roleId">The ID of the role to filter users by.</param>
    /// <param name="assignedBchId">The ID of the assigned bch to filter users by. Can be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an IEnumerable of User objects.</returns>
    Task<IEnumerable<User>> GetUsersByRole(string roleId, int? assignedBchId);
}
