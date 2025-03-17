using AuthenticationAPI.Api.Application.Common.Exceptions;
using AuthenticationAPI.Domain.ApplicationUser.Entities;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;
using AuthenticationAPI.Infrastructure.Settings;



namespace AuthenticationAPI.Api.Application.AuthenticationOtp.DemandeCode;

/// <summary>
/// Handler for processing the DemandeCodeCommand and generating a verification code.
/// </summary>
public class DemandeCodeHandler : IRequestHandler<DemandeCodeCommand, Unit>
{
    private readonly IOtpVerificationRepository _anonymousUserRepository;
    private readonly OtpSettings _settings;


    /// <summary>
    /// Initializes a new instance of the <see cref="DemandeCodeHandler"/> class.
    /// </summary>
    /// <param name="anonymousUserRepository">The repository for OTP verification.</param>
    /// <param name="settings">The OTP settings.</param>
    public DemandeCodeHandler(IOtpVerificationRepository anonymousUserRepository, OtpSettings settings)
    {
        _anonymousUserRepository = anonymousUserRepository ?? throw new ArgumentNullException(nameof(anonymousUserRepository));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    /// <summary>
    /// Handles the DemandeCodeCommand and generates a verification code.
    /// </summary>
    /// <param name="request">The DemandeCodeCommand containing the phone number.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The generated verification code.</returns>
    public async Task<Unit> Handle(DemandeCodeCommand request, CancellationToken cancellationToken)
    {
        int expiredCode = _settings.ExpiredCode;
        int allowedAttempts = _settings.AllowedAttemps;
        var lastRequests = await _anonymousUserRepository.GetItemsAsync(predicate: r => r.PhoneNumber == request.PhoneNumber, cancellationToken);
        var lastRequest = lastRequests.OrderByDescending(r => r.ExpiredOn).FirstOrDefault();

        if (lastRequest != null &&
            (lastRequest.ExpiredOn > long.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss")) || lastRequest.Attempts >= allowedAttempts))
        {
            throw new TooManyAttemptsException("Too many requests. Please try again later.");
        }

        var code = GenerateRandomCodeString();
        var AnonymUser = new OtpVerification()
        {
            Id = Guid.NewGuid().ToString(),
            PhoneNumber = request.PhoneNumber,
            Code = code,
            Attempts = lastRequest != null ? lastRequest.Attempts + 1 : 1,
            CreatedAt = long.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss")),
            ExpiredOn = long.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss")) + expiredCode,
            IsAuthenticated = false
        };
        await _anonymousUserRepository.AddItemAsync(AnonymUser, cancellationToken);
        return Unit.Value;
    }

    /// <summary>
    /// Generates a random code string for verification.
    /// </summary>
    /// <returns>The generated random code string.</returns>
    private static string GenerateRandomCodeString()
    {
        Random random = new Random();
        int randomCode = random.Next(100000, 1000000);
        return randomCode.ToString("D6");
    }
}

