using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;

namespace Application.Features.BlogPosts.Commands.UpdateBlogPost;

/// <summary>
/// Command for updating blog posts
/// </summary>
/// <param name="UpdateBlogPostDto">Incoming request.</param>
/// <param name="Id">Unique identifier of the blog post to be updated.</param>
public sealed record UpdateBlogPostCommand(UpdateBlogPostDto UpdateBlogPostDto, int Id) : ICommand
{
    /// <summary>
    /// Title of the blog post.
    /// </summary>
    public readonly string Title = UpdateBlogPostDto.Title;

    /// <summary>
    /// Description of the blog post.
    /// </summary>
    public readonly string Description = UpdateBlogPostDto.Description;
}