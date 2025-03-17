using Als.Foundation.Data.EntityFramework;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;
using AuthenticationAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Infrastructure.Repositories;

/// <summary>
/// Represents the repository for managing <see cref="User"/> entities.
/// </summary>
public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">The database context to be used by this repository.</param>
    /// <exception cref="ArgumentNullException">Thrown when the context is null.</exception>
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<User?> GetUserByName(string userName)
    {
        return await _context.Users
            //.Include(u => u.AssignedBch)
           // .Include(u => u.AssignedSecurityStation)
            //.ThenInclude(st => st!.SecurityStationBchs)
          //  .Include(u => u.AssignedAuthorityStation)
           // .ThenInclude(st => st!.AuthorityStationBchs)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetUsersByRole(string role, int? assignedBchId)
    {
        var roleIdParam = new Microsoft.Data.SqlClient.SqlParameter("@RoleName", role ?? (object)DBNull.Value);
        var assignedBchIdParam = new Microsoft.Data.SqlClient.SqlParameter("@AssignedBchId", assignedBchId ?? (object)DBNull.Value);

        var users = await _context.Users
            .FromSqlRaw("EXEC dbo.GetUserByRoleAndAssignedBchId @RoleName, @AssignedBchId", roleIdParam, assignedBchIdParam)
            .ToListAsync();

        return users;
    }

}