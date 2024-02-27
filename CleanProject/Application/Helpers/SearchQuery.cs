namespace Application.Helpers;

/// <summary>
/// Represents a search query.
/// </summary>
/// <param name="SearchTerm">Term to search for.</param>
/// <param name="SortColumn">Name of the column to sort on.</param>
/// <param name="SortOrder">Sort order (ascending/descending).</param>
/// <param name="Page">Number of the page.</param>
/// <param name="PageSize">Size of the page.</param>
public sealed record SearchQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize
);