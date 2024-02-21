using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;

namespace Application.Features.BlogPosts.Commands.UpdateBlogPost;

public sealed record UpdateBlogPostCommand(UpdateBlogPostDto UpdateBlogPostDto, int Id) : ICommand
{
    public readonly string Title = UpdateBlogPostDto.Title;
    public readonly string Description = UpdateBlogPostDto.Description;
}