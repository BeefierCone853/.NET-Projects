using Application.Abstractions.Messaging;

namespace Application.Features.BlogPosts.Commands.DeleteBlogPost;

/// <summary>
/// Command for deleting blog posts.
/// </summary>
/// <param name="BlogPostId">Unique identifier for the blog post which is to be deleted.</param>
public sealed record DeleteBlogPostCommand(int BlogPostId) : ICommand;