using AuthenticationAPI.Api.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = AuthenticationAPI.Api.Application.Common.Exceptions.ValidationException;

namespace AuthenticationAPI.Api.Filters;

/// <summary>
/// Represents an attribute implementing the IExceptionFilter interface to globally handle exceptions within an API.
/// </summary>
public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    /// <summary>
    /// Dictionary containing registered exception types and their respective handlers.
    /// </summary>
    private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiExceptionFilter"/> class.
    /// Registers known exception types and their associated handlers.
    /// </summary>
    /// <remarks>
    /// The known exception types and their respective handlers are added to the internal dictionary of the <see cref="ApiExceptionFilter"/> class
    /// to manage and handle exceptions uniformly within the API.
    /// </remarks>
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(Exception), HandleGlobalException }
        };
    }

    /// <summary>
    /// Handles exceptions caught in the application.
    /// </summary>
    /// <param name="context">The ExceptionContext containing information about the exception.</param>
    public void OnException(ExceptionContext context)
    {
        HandleException(context);
    }

    /// <summary>
    /// Handles the incoming exception based on its type by invoking the corresponding registered handler.
    /// </summary>
    /// <param name="context">The ExceptionContext containing information about the exception.</param>
    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.TryGetValue(type, out Action<ExceptionContext>? value))
            value.Invoke(context);
        else
            HandleGlobalException(context);
    }

    #region Exception Handlers

    /// <summary>
    /// Handles the ValidationException and returns a BadRequestObjectResult with validation details.
    /// </summary>
    /// <param name="context">The ExceptionContext containing information about the exception.</param>
    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Handles the NotFoundException and returns a NotFoundObjectResult with problem details.
    /// </summary>
    /// <param name="context">The ExceptionContext containing information about the exception.</param>
    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Handles the UnauthorizedAccessException and returns an ObjectResult with unauthorized details.
    /// </summary>
    /// <param name="context">The ExceptionContext containing information about the exception.</param>
    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var exception = (UnauthorizedAccessException)context.Exception;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Detail = exception.Message
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Handles an unhandled exception by creating a generic error response for internal server errors.
    /// Logs the exception details and sets the appropriate HTTP status code.
    /// </summary>
    /// <param name="context">The ExceptionContext containing information about the exception.</param>
    private void HandleGlobalException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An internal error occurred",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        _logger.LogError(context.Exception.ToString());
        context.ExceptionHandled = true;
    }

    #endregion
}
