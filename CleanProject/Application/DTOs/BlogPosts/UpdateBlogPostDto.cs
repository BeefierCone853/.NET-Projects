using Application.Abstractions;

namespace Application.DTOs.BlogPosts;

public sealed record UpdateBlogPostDto(string Title, string Description) : IBlogPostDto;