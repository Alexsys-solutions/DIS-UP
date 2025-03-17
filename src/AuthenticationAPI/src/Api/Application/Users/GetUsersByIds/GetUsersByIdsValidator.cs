namespace AuthenticationAPI.Api.Application.Users.GetUsersByIds;

/// <summary>
/// Validator for the <see cref="GetUsersByIdsQuery"/> class.
/// </summary>
public class GetUsersByIdsValidator : AbstractValidator<GetUsersByIdsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersByIdsValidator"/> class.
    /// </summary>
    public GetUsersByIdsValidator()
    {
        // Define validation rules for the 'Ids' property of GetAhByIdsQuery
        RuleFor(query => query.Ids)
            .NotEmpty().WithMessage("The 'Ids' list must not be empty.")
            .Must(ids => ids != null && ids.Length > 0).WithMessage("The 'Ids' list must contain at least one element.");
    }
}
