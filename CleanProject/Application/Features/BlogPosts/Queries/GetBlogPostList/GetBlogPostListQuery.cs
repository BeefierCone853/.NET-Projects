using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;
using Application.Helpers;

namespace Application.Features.BlogPosts.Queries.GetBlogPostList;

/// <summary>
/// Query for retrieving all blog posts.
/// </summary>
/// <param name="SearchQuery">Query parameters.</param>
public sealed record GetBlogPostListQuery(SearchQuery SearchQuery) : IQuery<PagedList<BlogPostDto>>
{
    /// <summary>
    /// Number of the page.
    /// </summary>
    public readonly int Page = SearchQuery.Page;
}