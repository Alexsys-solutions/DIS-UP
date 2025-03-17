namespace AuthenticationAPI.Api.Application.Users.Login;

/// <summary>
/// Validator for the <see cref="LoginCommand"/> class.
/// </summary>
public class LoginValidator : AbstractValidator<LoginCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginValidator"/> class.
    /// </summary>
    public LoginValidator()
    {
        // Validation rule for UserName
        RuleFor(command => command.UserName).NotEmpty().WithMessage("Username is required.");

        // Validation rules for Password
        RuleFor(command => command.Password).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-zA-Z0-9]").WithMessage("Password must contain at least one alphanumeric character.");
    }
}
