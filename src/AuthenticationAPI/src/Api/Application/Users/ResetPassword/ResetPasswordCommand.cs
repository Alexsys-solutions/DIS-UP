namespace AuthenticationAPI.Api.Application.Users.ResetPassword;

public class ResetPasswordCommand : IRequest<Unit>
{
    public required string UserId { get; init; }
    public required string NewPassword { get; init; }
    public required string ConfirmPassword { get; set; }

}
