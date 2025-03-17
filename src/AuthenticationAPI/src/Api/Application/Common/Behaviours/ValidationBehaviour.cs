using ValidationException = AuthenticationAPI.Api.Application.Common.Exceptions.ValidationException;

namespace AuthenticationAPI.Api.Application.Common.Behaviours;

/// <summary>
/// Pipeline behavior for request validation. Validates the incoming request using a set of validators.
/// If validation fails, it throws a ValidationException with details of the validation failures.
/// </summary>
/// <typeparam name="TRequest">The type of request to be validated.</typeparam>
/// <typeparam name="TResponse">The type of response from the validated request.</typeparam>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehaviour{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">Collection of validators to validate the request.</param>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        ArgumentNullException.ThrowIfNull(validators);
        _validators = validators;
    }

    /// <summary>
    /// Handles the incoming request by validating it using the specified validators.
    /// If validation fails, it throws a ValidationException with details of the validation failures.
    /// </summary>
    /// <param name="request">The request to be validated.</param>
    /// <param name="next">Delegate to the next handler in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns the response from the validated request.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Count != 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }
        return await next();
    }
}
