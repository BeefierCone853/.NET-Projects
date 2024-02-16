using Domain.Primitives;

namespace Domain.Entities;

/// <summary>
/// Entity which represents blog posts.
/// </summary>
public sealed class BlogPost : Entity
{
    /// <summary>
    /// Title of a blog post.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Description of a blog post.
    /// </summary>
    public string Description { get; set; }
}