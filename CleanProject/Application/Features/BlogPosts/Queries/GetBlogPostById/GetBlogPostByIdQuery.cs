using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;

namespace Application.Features.BlogPosts.Queries.GetBlogPostById;

/// <summary>
/// Query for getting a blog post with a matching unique identifier.
/// </summary>
/// <param name="Id">Unique identifier of the blog post.</param>
public sealed record GetBlogPostByIdQuery(int Id) : IQuery<BlogPostDto>;