namespace AuthenticationAPI.Api.Application.Common.Models;

/// <summary>
/// Represents a paginated response containing a subset of elements for a specific page.
/// </summary>
/// <typeparam name="T">The type of elements in the paginated response.</typeparam>
public class PaginatedResponse<T>
{
    /// <summary>
    /// Page number.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Total number of items.
    /// </summary>
    public long TotalItems { get; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// List of elements for the current page.
    /// </summary>
    public IEnumerable<T> Data { get; }

    /// <summary>
    /// Constructor for the PagedResponse class.
    /// </summary>
    /// <param name="data">List of elements for the page.</param>
    /// <param name="pageNumber">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="totalItems">Total number of items.</param>
    public PaginatedResponse(IEnumerable<T> data, int pageNumber, int pageSize, long totalItems)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        Data = data;
    }
}
