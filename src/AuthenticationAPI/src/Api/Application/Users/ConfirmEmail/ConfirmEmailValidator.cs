namespace AuthenticationAPI.Api.Application.Users.ConfirmEmail;

/// <summary>
/// Validator for the <see cref="ConfirmEmailCommand"/> class.
/// </summary>
public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailValidator()
    {
        RuleFor(command => command.UserId).NotEmpty().WithMessage("User ID is required.");
        RuleFor(command => command.Token).NotEmpty().WithMessage("Token is required.");
    }
}
