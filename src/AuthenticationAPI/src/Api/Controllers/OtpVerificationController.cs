using AuthenticationAPI.Api.Application.AuthenticationOtp.DemandeCode;
using AuthenticationAPI.Api.Application.AuthenticationOtp.VerifyCode;

namespace AuthenticationAPI.Api.Controllers;

/// <summary>
/// Controller for verifying user phone numbers and sending verification codes.
/// </summary>
[Route("anonym-users")]
[ApiController]
public class OtpVerificationController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor for OtpVerificationController.
    /// </summary>
    /// <param name="mediator">The mediator for handling commands and queries.</param>
    public OtpVerificationController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Requests a verification code for the user's phone number.
    /// </summary>
    /// <param name="command">The command containing the necessary data for the code request.</param>
    /// <returns>The result of the code request.</returns>
    [HttpPost("request-code")]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RequestVerificationCode([FromBody] DemandeCodeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Verifies the code for the user's phone number.
    /// </summary>
    /// <param name="command">The command containing the necessary data for code verification.</param>
    /// <returns>The result of the code verification.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeCommand command)
    {
        var res = await _mediator.Send(command);
        return Ok(res);
    }
}
