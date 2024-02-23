using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

/// <summary>
/// Command for creating blog posts.
/// </summary>
/// <param name="CreateBlogPostDto">Incoming request.</param>
public sealed record CreateBlogPostCommand(CreateBlogPostDto CreateBlogPostDto) : ICommand<int>
{
    /// <summary>
    /// Title of the blog post.
    /// </summary>
    public readonly string Title = CreateBlogPostDto.Title;

    /// <summary>
    /// Description of the blog post.
    /// </summary>
    public readonly string Description = CreateBlogPostDto.Description;
}