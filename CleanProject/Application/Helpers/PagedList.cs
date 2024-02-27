using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers;

/// <summary>
/// Represents a paged list.
/// </summary>
/// <typeparam name="T">Generic object.</typeparam>
public class PagedList<T>
{
    /// <summary>
    /// Creates a <see cref="PagedList{T}"/>>.
    /// </summary>
    /// <param name="items">List of objects.</param>
    /// <param name="page">Number of the page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="totalCount">Total count of the objects.</param>
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    [JsonConstructor]
    private PagedList(List<T> items, bool hasNextPage, bool hasPreviousPage)
    {
        Items = items;
    }

    /// <summary>
    /// List of objects.
    /// </summary>
    public List<T> Items { get; }

    /// <summary>
    /// Number of the page.
    /// </summary>
    private int Page { get; }

    /// <summary>
    /// Size of the page.
    /// </summary>
    private int PageSize { get; }

    /// <summary>
    /// Total count of the objects.
    /// </summary>
    private int TotalCount { get; }

    /// <summary>
    /// Indicates of there is a next page.
    /// </summary>
    public bool HasNextPage => Page * PageSize < TotalCount;

    /// <summary>
    /// Indicates of there is a previous page.
    /// </summary>
    public bool HasPreviousPage => Page > 1;

    /// <summary>
    /// Creates a <see cref="PagedList{T}"/>
    /// </summary>
    /// <param name="query">Database query.</param>
    /// <param name="page">Number of the page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <returns>A newly created <see cref="PagedList{T}"/> with a collection of paged items.</returns>
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}