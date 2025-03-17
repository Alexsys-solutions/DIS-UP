using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Api.Application.Users.ConfirmEmail;

/// <summary>
/// Handler for confirming a user's email.
/// </summary>
public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, string>
{
    private readonly UserManager<User> _userManager;

    public ConfirmEmailHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId) ?? throw new KeyNotFoundException("User not found.");
        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        if (result.Succeeded)
        {
            return "Email confirmed successfully.";
        }
        else
        {
            // Throw an exception with error details if email confirmation fails ex:token is invalid.
            var validationFailures = result.Errors.Select(error => new ValidationFailure(error.Code, error.Description));
            throw new Common.Exceptions.ValidationException(validationFailures);
        }
    }
}