namespace AuthenticationAPI.Api.Application.Users.Register;

/// <summary>
/// Validator for the <see cref="RegisterCommand"/> class.
/// </summary>
public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterValidator"/> class.
    /// </summary>
    public RegisterValidator()
    {
        // Validation rule for UserName
        RuleFor(command => command.UserName).NotEmpty().WithMessage("Username is required.");

        // Validation rule for FirstName
        RuleFor(command => command.FirstName).NotEmpty().WithMessage("French First name is required.");

        // Validation rule for LastName
        RuleFor(command => command.LastName).NotEmpty().WithMessage("French Last name is required.");

        // Validation rule for FirstNameAr
        RuleFor(command => command.FirstNameAr).NotEmpty().WithMessage("Arab First name is required.");

        // Validation rule for LastNameAr
        RuleFor(command => command.LastNameAr).NotEmpty().WithMessage("Arab Last name is required.");

        // Validation rules for Password
        RuleFor(command => command.Password).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-zA-Z0-9]").WithMessage("Password must contain at least one alphanumeric character.");

        // Validation rules for PhoneNumber
        RuleFor(command => command.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches("^(06|07)\\d{8}$").WithMessage("Invalid phone number format. It should start with 06 or 07 and contain 10 digits.");
        
        // Validation rules for Email
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        // Validation rules for Role
        RuleFor(x => x.Roles)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Roles must not be null")
            .NotEmpty().WithMessage("At least one role is required")
            .Must(roles => roles.TrueForAll(role => IsValidRole(role))).WithMessage("Invalid role. Must be 'AgentBch' or 'Observer' or 'Acheteur' or 'Admin'");
    }

    /// <summary>
    /// Checks if the provided role is a valid role.
    /// </summary>
    /// <param name="role">The role to validate.</param>
    /// <returns><c>true</c> if the role is valid; otherwise, <c>false</c>.</returns>
    private static bool IsValidRole(string role)
    {
        return role == "AgentBch" || role == "Observer" || role == "Acheteur" || role == "Admin" || role == "SecurityOfficer" || role == "Agent" || role == "Other";
    }
}
