using AuthenticationAPI.Api.Application.Common.Exceptions;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Api.Application.Users.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly UserManager<User> _userManager;

    public ResetPasswordHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId) ??
            throw new NotFoundException("User Not found!!");
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
        return Unit.Value;
    }
}
