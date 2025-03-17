using Als.Foundation.Data.CosmosDb;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;

namespace AuthenticationAPI.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for phone verification operations on anonymous users.
/// </summary>
public class OtpVerificationRepository : BaseRepository<OtpVerification>, IOtpVerificationRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OtpVerificationRepository"/> class.
    /// </summary>
    /// <param name="cosmosDbService">The Cosmos DB service used by the repository.</param>
    public OtpVerificationRepository(ICosmosDbService cosmosDbService) : base(cosmosDbService)
    {

    }
}
