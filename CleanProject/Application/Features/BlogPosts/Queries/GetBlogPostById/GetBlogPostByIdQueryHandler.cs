using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;
using AutoMapper;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Queries.GetBlogPostById;

/// <summary>
/// Handler for getting a blog post with a matching unique identifier.
/// </summary>
/// <param name="blogPostRepository">Repository for performing database operations on <see cref="IBlogPostRepository"/> entity.</param>
/// <param name="mapper">Maps the retrieved blog post entity to a DTO.</param>
internal sealed class GetBlogPostByIdQueryHandler(
    IBlogPostRepository blogPostRepository,
    IMapper mapper)
    : IQueryHandler<GetBlogPostByIdQuery, BlogPostDto>
{
    /// <summary>
    /// Handles the retrieval of the blog post with a matching unique identifier.
    /// </summary>
    /// <param name="request">Contains the ID of the blog post to be retrieved.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <remarks><see cref="BlogPostsErrors.NotFound"/> is returned if the blog post doesn't exist.</remarks>
    /// <returns><see cref="Result"/> with a <see cref="BlogPostDto"/> with the matching ID from the request.</returns>
    public async Task<Result<BlogPostDto>> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var blogPost = await blogPostRepository.GetById(request.Id);
        if (blogPost is null)
        {
            return Result.Failure<BlogPostDto>(BlogPostsErrors.NotFound(request.Id));
        }

        var response = mapper.Map<BlogPostDto>(blogPost);
        return response;
    }
}