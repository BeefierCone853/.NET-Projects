namespace Application.Features.BlogPosts.DTOs;

/// <summary>
/// Represents a DTO for updating blog posts.
/// </summary>
/// <param name="Title">Title of the blog post.</param>
/// <param name="Description">Description of the blog post.</param>
public sealed record UpdateBlogPostDto(string Title, string Description) : IBlogPostDto;