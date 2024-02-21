namespace Application.Features.BlogPosts.DTOs;

public sealed record CreateBlogPostDto(string Title, string Description): IBlogPostDto;