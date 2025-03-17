using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Api.Application.Roles.GetAllRoles
{
    /// <summary>
    /// Handler for retrieving all roles.
    /// </summary>
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<GetAllRolesResponse>>
    {
        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRolesHandler"/> class.
        /// </summary>
        /// <param name="roleManager">The role manager to retrieve roles.</param>
        public GetAllRolesHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Handles the request to retrieve all roles.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of all roles.</returns>
        public async Task<List<GetAllRolesResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
            var response = roles.Adapt<List<GetAllRolesResponse>>();
            return response;
        }
    }
}
