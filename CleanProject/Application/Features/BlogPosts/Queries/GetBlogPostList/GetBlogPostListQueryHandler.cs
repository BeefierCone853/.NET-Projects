using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Queries.GetBlogPostList;

/// <summary>
/// Handler for getting all blog posts.
/// </summary>
/// <param name="blogPostRepository">Repository for performing database operations on <see cref="IBlogPostRepository"/> entity.</param>
internal sealed class GetBlogPostListQueryHandler(
    IBlogPostRepository blogPostRepository) : IQueryHandler<GetBlogPostListQuery, List<BlogPostDto>>
{
    /// <summary>
    /// Handles the retrieval of all the blog posts.
    /// </summary>
    /// <param name="request">Signals the handler to retrieve the blog posts.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns><see cref="Result"/> with a list of <see cref="BlogPostDto"/>s or empty array.</returns>
    public async Task<Result<List<BlogPostDto>>> Handle(GetBlogPostListQuery request,
        CancellationToken cancellationToken)
    {
        var blogPosts = await blogPostRepository.GetAll();
        var response = blogPosts.Select(blogPost => new BlogPostDto(
            Id: blogPost.Id,
            Title: blogPost.Title,
            Description: blogPost.Description)).ToList();
        return response;
    }
}