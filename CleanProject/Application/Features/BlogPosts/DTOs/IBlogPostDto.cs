namespace Application.Features.BlogPosts.DTOs;

/// <summary>
/// Defines common properties for a blog post DTO.
/// </summary>
public interface IBlogPostDto
{
    
    /// <summary>
    /// Title of the blog post.
    /// </summary>
    string Title { get; init; }
    
    /// <summary>
    /// Description of the blog post.
    /// </summary>
    string Description { get; init; }
}