using AuthenticationAPI.Domain.ApplicationUser.Entities;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;
using AuthenticationAPI.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Api.Application.Users.Register;

/// <summary>
/// Handles the registration command and processes user registration.
/// </summary>
public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Constructor for RegisterHandler.
    /// </summary>
    /// <param name="userManager">The UserManager for managing user-related operations.</param>
    /// <param name="emailService">The email service for sending confirmation emails.</param>
    public RegisterHandler(UserManager<User> userManager, IEmailService emailService)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    /// <summary>
    /// Handles the registration command and processes user registration.
    /// </summary>
    /// <param name="request">The RegisterCommand request.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A string indicating the result of the registration operation.</returns>
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Adapt the RegisterCommand to a User entity
        var user = request.Adapt<User>();

        // Create the user with the specified password
        var result = await _userManager.CreateAsync(user, request.Password);

        // Check if user creation was successful
        if (result.Succeeded)
        {
            // Add roles to the user
            foreach (string role in request.Roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            // Generate email confirmation token if needed
            // var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your email by clicking this link: http://localhost:5000/users/confirm-email?token={emailToken}&userId={user.Id}");

            return user.Id;
        }
        else
        {
            // Throw an exception with error details if user creation fails
            var validationFailures = result.Errors.Select(error => new ValidationFailure(error.Code, error.Description));
            throw new Common.Exceptions.ValidationException(validationFailures);
        }
    }
}
