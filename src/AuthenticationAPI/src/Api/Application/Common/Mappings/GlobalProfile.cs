using AuthenticationAPI.Api.Application.Common.Models;
using AuthenticationAPI.Api.Application.Users.Register;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
namespace AuthenticationAPI.Api.Application.Common.Mappings;

/// <summary>
/// Represents a global profile used for configuring type adapters.
/// </summary>
public class GlobalProfile : IRegister
{
    /// <summary>
    /// Registers type adapters for mapping between types.
    /// </summary>
    /// <param name="config">The type adapter configuration.</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterCommand, User>()
              .Ignore(dest => dest.UserRoles);

        config.NewConfig<User, UserResponse>();
            //.Map(dest => dest.UserAssignment, src => src.GetUserAssignmentEntity());
    }
}
