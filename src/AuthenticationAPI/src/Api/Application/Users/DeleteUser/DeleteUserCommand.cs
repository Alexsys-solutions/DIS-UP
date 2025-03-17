namespace AuthenticationAPI.Api.Application.Users.DeleteUser;

public class DeleteUserCommand : IRequest<Unit>
{
    public required string UserId { get; set; }
}
