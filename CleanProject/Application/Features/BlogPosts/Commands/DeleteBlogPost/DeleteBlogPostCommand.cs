using Application.Abstractions.Messaging;

namespace Application.Features.BlogPosts.Commands.DeleteBlogPost;

public sealed record DeleteBlogPostCommand(int BlogPostId) : ICommand;