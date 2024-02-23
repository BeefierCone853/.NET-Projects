using Domain.Primitives;

namespace Application.Features.BlogPosts.DTOs;

/// <summary>
/// Represents the standard DTO of a blog post.
/// </summary>
/// <param name="Title">Title of the blog post.</param>
/// <param name="Description">Description of the blog post.</param>
/// <param name="Id">Unique identifier of the blog post.</param>
public sealed record BlogPostDto(string Title, string Description, int Id) : BaseDto(Id), IBlogPostDto;