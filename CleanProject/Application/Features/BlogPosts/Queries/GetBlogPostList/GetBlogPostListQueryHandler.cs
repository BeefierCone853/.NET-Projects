using System.Linq.Expressions;
using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;
using Application.Helpers;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Queries.GetBlogPostList;

/// <summary>
/// Handler for getting all blog posts.
/// </summary>
/// <param name="blogPostRepository">Repository for performing database operations on <see cref="IBlogPostRepository"/> entity.</param>
internal sealed class GetBlogPostListQueryHandler(
    IBlogPostRepository blogPostRepository) : IQueryHandler<GetBlogPostListQuery, PagedList<BlogPostDto>>
{
    /// <summary>
    /// Handles the retrieval of all the blog posts.
    /// </summary>
    /// <param name="request">Signals the handler to retrieve the blog posts.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns><see cref="Result"/> with a list of <see cref="BlogPostDto"/>s or empty array.</returns>
    public async Task<Result<PagedList<BlogPostDto>>> Handle(
        GetBlogPostListQuery request,
        CancellationToken cancellationToken)
    {
        var blogPostsQuery = blogPostRepository.GetQueryable();
        if (!string.IsNullOrWhiteSpace(request.SearchQuery.SearchTerm))
        {
            blogPostsQuery = blogPostsQuery.Where(blogPost =>
                blogPost.Title.Contains(request.SearchQuery.SearchTerm) ||
                blogPost.Description.Contains(request.SearchQuery.SearchTerm));
        }

        blogPostsQuery = request.SearchQuery.SortOrder?.ToLower() == "desc"
            ? blogPostsQuery.OrderByDescending(GetSortProperty(request))
            : blogPostsQuery.OrderBy(GetSortProperty(request));

        var blogPostResponseQuery = blogPostsQuery
            .Select(blogPost => new BlogPostDto(
                blogPost.Title,
                blogPost.Description,
                blogPost.Id
            ));
        var blogPosts = await PagedList<BlogPostDto>.CreateAsync(
            query: blogPostResponseQuery,
            page: request.Page,
            pageSize: request.SearchQuery.PageSize);
        return blogPosts;
    }

    private static Expression<Func<BlogPost, object>> GetSortProperty(GetBlogPostListQuery request) =>
        request.SearchQuery.SortColumn?.ToLower() switch
        {
            "title" => blogPost => blogPost.Title,
            "description" => blogPost => blogPost.Description,
            _ => blogPost => blogPost.Id
        };
}