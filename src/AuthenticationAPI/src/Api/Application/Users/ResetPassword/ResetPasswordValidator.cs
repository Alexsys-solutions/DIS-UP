namespace AuthenticationAPI.Api.Application.Users.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-zA-Z0-9]").WithMessage("Password must contain at least one alphanumeric character.");
        RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword)
              .WithMessage("Confirm Password must match New Password.");
    }
}
