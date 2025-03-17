using Als.Foundation.Data.Abstractions.CosmosDb;
using AuthenticationAPI.Domain.ApplicationUser.Entities;

namespace AuthenticationAPI.Domain.ApplicationUser.Interfaces;

/// <summary>
/// Represents a repository for verifying phone number.
/// </summary>
public interface IOtpVerificationRepository : IBaseRepository<OtpVerification>
{
}
