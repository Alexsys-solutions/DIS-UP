using AuthenticationAPI.Api.Application.Common.Exceptions;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Api.Application.Users.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly UserManager<User> _userManager;

    public DeleteUserHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            throw new NotFoundException($"User with ID {request.UserId} not found.");
        await _userManager.DeleteAsync(user);
        return Unit.Value;
    }
}