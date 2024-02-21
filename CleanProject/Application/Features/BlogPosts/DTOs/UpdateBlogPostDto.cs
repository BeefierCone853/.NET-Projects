namespace Application.Features.BlogPosts.DTOs;

public sealed record UpdateBlogPostDto(string Title, string Description) : IBlogPostDto;