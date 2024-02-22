using Domain.Shared;

namespace Domain.Features.BlogPosts;

/// <summary>
/// Contains common errors for blog posts.
/// </summary>
public static class BlogPostsErrors
{
    /// <summary>
    /// Creates a not found error.
    /// </summary>
    /// <param name="blogPostId">Unique identifier of the blog post.</param>
    /// <returns>Error containing code and description of the error.</returns>
    public static Error NotFound(int blogPostId) => Error.NotFound(
        "BlogPost.NotFound",
        $"The blog post with Id {blogPostId} was not found.");
}