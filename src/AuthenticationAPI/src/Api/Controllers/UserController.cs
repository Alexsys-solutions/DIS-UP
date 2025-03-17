using AuthenticationAPI.Api.Application.Common.Models;
using AuthenticationAPI.Api.Application.Roles.GetAllRoles;
using AuthenticationAPI.Api.Application.Users.ConfirmEmail;
using AuthenticationAPI.Api.Application.Users.DeleteUser;
using AuthenticationAPI.Api.Application.Users.GetAllUsers;
using AuthenticationAPI.Api.Application.Users.GetUsersByIds;
using AuthenticationAPI.Api.Application.Users.GetUsersByRole;
using AuthenticationAPI.Api.Application.Users.LockoutUser;
using AuthenticationAPI.Api.Application.Users.Login;
using AuthenticationAPI.Api.Application.Users.Register;
using AuthenticationAPI.Api.Application.Users.ResetPassword;
using AuthenticationAPI.Api.Application.Users.UnlockUser;

namespace AuthenticationAPI.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _mediator;
    /// <summary>
    /// Constructor for AuthController.
    /// </summary>
    /// <param name="mediator">The mediator for handling commands and queries.</param>
    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="command">The login command.</param>
    /// <returns>Returns a token.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return BadRequest(ex.Message);
        }

    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="command">The register command.</param>
    /// <returns>Returns a message.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { Id = result });
    }

    /// <summary>
    /// Confirms a user's email.
    /// </summary>
    /// <param name="token">The command containing the confirmation token.</param>
    /// <param name="userId">The command containing the user ID.</param>
    /// <returns>A response indicating the result of the email confirmation.</returns>
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
    {
        var command = new ConfirmEmailCommand
        {
            Token = token,
            UserId = userId
        };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>Returns a list of users.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUsers([FromQuery] string? roleId, [FromQuery] int? assignedBchId,
        [FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? userName)
    {
        var query = new GetAllUsersQuery(roleId, assignedBchId, pageNumber, pageSize, userName);
        var users = await _mediator.Send(query);
        return Ok(users);
    }

    /// <summary>
    /// Gets users by their role and assigned BCH ID.
    /// </summary>
    /// <param name="role">The role of the users to retrieve.</param>
    /// <param name="assignedBchId">The assigned BCH ID for filtering.</param>
    /// <returns>Returns users based on the specified role and assigned BCH ID.</returns>
    [HttpGet("by-role/{role}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersByRole([FromRoute] string role, [FromQuery] int? assignedBchId)
    {
        var query = new GetUsersByRoleQuery(role, assignedBchId);
        var users = await _mediator.Send(query);
        return Ok(users);
    }

    /// <summary>
    /// Gets users by their Ids .
    /// </summary>
    /// <param name="Ids">The Ids of the users to retrieve.</param>
    /// <returns>Returns users based on the specified Ids.</returns>
    [HttpGet("multiple")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersByIds([FromQuery] string[] Ids)
    {
        var query = new GetUsersByIdsQuery(Ids);
        var users = await _mediator.Send(query);
        return Ok(users);
    }

    /// <summary>
    /// Retrieves all roles.
    /// </summary>
    /// <returns>An action result containing the roles.</returns>
    [HttpGet("roles")]
    public async Task<IActionResult> GetAllRoles()
    {
        var query = new GetAllRolesQuery();
        var roles = await _mediator.Send(query);
        return Ok(roles);
    }

    /// <summary>
    /// Locks out a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to lock out.</param>
    /// <returns>An action result indicating success.</returns>
    [HttpGet("lockout/{id}")]
    public async Task<IActionResult> LockoutUser([FromRoute] string id)
    {
        var query = new LockoutUserQuery(id);
        var res = await _mediator.Send(query);
        return Ok(res);
    }

    /// <summary>
    /// Unlocks a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to unlock.</param>
    /// <returns>An action result indicating success.</returns>
    [HttpGet("unlock/{id}")]
    public async Task<IActionResult> UnlockUser([FromRoute] string id)
    {
        var query = new UnlockUserQuery(id);
        var res = await _mediator.Send(query);
        return Ok(res);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var res = await _mediator.Send(command);
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string id)
    {
        var command = new DeleteUserCommand { UserId = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
