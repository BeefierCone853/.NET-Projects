namespace Application.Features.BlogPosts.DTOs;

/// <summary>
/// Represents a DTO for creating blog posts.
/// </summary>
/// <param name="Title">Title of the blog post.</param>
/// <param name="Description">Description of the blog post.</param>
public sealed record CreateBlogPostDto(string Title, string Description) : IBlogPostDto;