namespace AuthenticationAPI.Api.Application.AuthenticationOtp.VerifyCode
{
    /// <summary>
    /// Represents a command to verify the verification code of an anonymous user.
    /// </summary>
    public class VerifyCodeCommand : IRequest<string>
    {
        /// <summary>
        /// Gets or sets the verification code.
        /// </summary>
        public string Code { get; set; } = default!;
    }
}