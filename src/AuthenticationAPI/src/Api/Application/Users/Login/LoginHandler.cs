using AuthenticationAPI.Api.Application.Common.Models;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Api.Application.Users.Login;

/// <summary>
/// Handles the login command and generates a response.
/// </summary>
public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserRepository _userRepository;


    public LoginHandler(
        SignInManager<User> signInManager,
        ITokenProvider tokenProvider,
        IUserRepository userRepository
       )
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(signInManager));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Handles the login command and generates a login response.
    /// </summary>
    /// <param name="request">The LoginCommand request.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A LoginResponse containing authentication information.</returns>
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Find the user by username
            var user = await _userRepository.GetUserByName(request.UserName);

            // Check if the user exists and the password is correct
            if (user != null)
            {
                // Sign in the user
                var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);

                // Check if the sign-in was successful
                if (result.Succeeded)
                {
                    // Generate a JWT token
                    var token = _tokenProvider.GenerateAccessToken(user);

                    // Adapt user entity to UserResponse model
                    var userResponse = user.Adapt<UserResponse>();

                    // Create the login response
                    var response = new LoginResponse()
                    {
                        User = userResponse,
                        AccessToken = token,
                        IsAutheticated = true,
                        Message = "Logged In Successfully",
                        Roles = user.GetRoleNames(),
                    };

                    return response;
                }

            }        
            // Return invalid credential if the login is unsuccessful
            return new LoginResponse { AccessToken = null, IsAutheticated = false, Message = "Invalid username or password" };

        }
        catch (Exception ex)
        {
            return new LoginResponse { AccessToken = null, IsAutheticated = false, Message = "Invalid username or password" };

        }
    }
}
