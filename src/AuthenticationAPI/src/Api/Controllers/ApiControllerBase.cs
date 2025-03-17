namespace AuthenticationAPI.Api.Controllers;

/// <summary>
/// Base controller for API controllers.
/// </summary>
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ISender _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiControllerBase"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for handling requests.</param>
    protected ApiControllerBase(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
}
