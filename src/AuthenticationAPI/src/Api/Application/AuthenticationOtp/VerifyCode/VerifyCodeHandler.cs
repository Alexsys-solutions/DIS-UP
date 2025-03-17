
using AuthenticationAPI.Api.Application.Common.Exceptions;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;

namespace AuthenticationAPI.Api.Application.AuthenticationOtp.VerifyCode;

/// <summary>
/// Handler for verifying the verification code of an anonymous user.
/// </summary>
public class VerifyCodeHandler : IRequestHandler<VerifyCodeCommand, string>
{
    private readonly IOtpVerificationRepository _anonymousUserRepository;
    private readonly ITokenProvider _tokenProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="VerifyCodeHandler"/> class.
    /// </summary>
    /// <param name="anonymousUserRepository">The repository for managing anonymous users.</param>
    /// <param name="tokenProvider">the tokenProviding for generating tokens</param>
    public VerifyCodeHandler(IOtpVerificationRepository anonymousUserRepository, ITokenProvider tokenProvider)
    {
        _anonymousUserRepository = anonymousUserRepository ?? throw new ArgumentNullException(nameof(anonymousUserRepository));
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
    }

    /// <summary>
    /// Handles the verification of the verification code.
    /// </summary>
    /// <param name="request">The verification code command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The anonymous user if the code is valid.</returns>
    /// <exception cref="NotFoundException">Thrown when the code is not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the code is expired.</exception>
    public async Task<string> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
    {
        var verification = (await _anonymousUserRepository.GetItemsAsync(r => r.Code == request.Code, cancellationToken)).FirstOrDefault();

        if (verification == null)
        {
            throw new NotFoundException("Invalid code. Please try again.");
        }

        var currentTimeStamp = long.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss"));

        if (verification.ExpiredOn < currentTimeStamp)
        {
            throw new InvalidOperationException("The code has expired!!!");
        }
        verification.IsAuthenticated = true;

        var updatedAnonymUser = await _anonymousUserRepository.UpdateItemAsync(verification.Id, verification, [verification.Id], cancellationToken);
        var token = _tokenProvider.GenerateOtpToken(updatedAnonymUser);
        return token;
    }
}
