namespace AuthenticationAPI.Api.Application.AuthenticationOtp.VerifyCode;

/// <summary>
/// Validator for the VerifyCodeCommand.
/// </summary>
public class VerifyCodeValidator : AbstractValidator<VerifyCodeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VerifyCodeValidator"/> class.
    /// </summary>
    public VerifyCodeValidator()
    {
        RuleFor(command => command.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Matches("[0-9]{6}").WithMessage("Code must contain 6 digits.");
    }
}
