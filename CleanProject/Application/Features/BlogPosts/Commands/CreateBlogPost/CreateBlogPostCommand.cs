using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

public sealed record CreateBlogPostCommand(CreateBlogPostDto CreateBlogPostDto) : ICommand<int>
{
    public readonly string Title = CreateBlogPostDto.Title;
    public readonly string Description = CreateBlogPostDto.Description;
}