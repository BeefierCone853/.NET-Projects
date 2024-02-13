using Application.Abstractions.Messaging;
using Application.DTOs.BlogPosts;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

public sealed record CreateBlogPostCommand(CreateBlogPostDto CreateBlogPostDto) : ICommand
{
    public readonly string Title = CreateBlogPostDto.Title;
    public readonly string Description = CreateBlogPostDto.Description;
}