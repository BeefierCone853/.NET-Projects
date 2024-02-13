using Application.Abstractions;

namespace Application.DTOs.BlogPosts;

public record UpdateBlogPostDto(string Title, string Description) : IBlogPostDto;