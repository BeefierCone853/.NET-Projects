using Application.Abstractions.Messaging;
using Application.DTOs.BlogPosts;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

public sealed record CreateBlogPostCommand(CreateBlogPostDto CreateBlogPostDto) : ICommand;