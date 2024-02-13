using Application.Abstractions;

namespace Application.DTOs.BlogPosts;

public sealed record CreateBlogPostDto(string Title, string Description): IBlogPostDto;