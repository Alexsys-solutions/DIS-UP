
namespace AuthenticationAPI.Api.Application.AuthenticationOtp.DemandeCode;

/// <summary>
/// Validator for the DemandeCodeCommand.
/// </summary>
public class DemandeCodeValidator : AbstractValidator<DemandeCodeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DemandeCodeValidator"/> class.
    /// </summary>
    public DemandeCodeValidator()
    {
        RuleFor(command => command.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches("^(07|06)[0-9]{8}$").WithMessage("Phone number must start with '07' or '06' and contain exactly 10 digits.");
    }
}
