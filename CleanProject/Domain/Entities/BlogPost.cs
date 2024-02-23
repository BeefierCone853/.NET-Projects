using Domain.Primitives;

namespace Domain.Entities;

/// <summary>
/// Represents a blog post entity.
/// </summary>
public sealed class BlogPost : Entity
{
    /// <summary>
    /// Title of the blog post.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Description of the blog post.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Updates current object with a new title and description.
    /// </summary>
    /// <param name="title">New title of a blog post.</param>
    /// <param name="description">New description of a blog post.</param>
    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }
}