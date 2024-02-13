using Application.Abstractions;

namespace Application.DTOs.BlogPosts;

public record CreateBlogPostDto(string Title, string Description): IBlogPostDto;