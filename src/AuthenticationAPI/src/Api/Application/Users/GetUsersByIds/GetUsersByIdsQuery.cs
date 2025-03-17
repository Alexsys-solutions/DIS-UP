using AuthenticationAPI.Api.Application.Common.Models;

namespace AuthenticationAPI.Api.Application.Users.GetUsersByIds;

/// <summary>
/// Represents a query to get users by a list of IDs.
/// </summary>
public class GetUsersByIdsQuery : IRequest<List<UserResponse>>
{
    /// <summary>
    /// Gets or sets the list of IDs.
    /// </summary>
    public string[] Ids { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersByIdsQuery"/> class.
    /// </summary>
    /// <param name="Ids">The list of IDs.</param>
    public GetUsersByIdsQuery(string[] Ids)
    {
        this.Ids = Ids;
    }
}
