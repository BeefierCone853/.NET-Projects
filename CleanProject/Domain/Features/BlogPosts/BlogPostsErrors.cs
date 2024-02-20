using Domain.Shared;

namespace Domain.Features.BlogPosts;

public static class BlogPostsErrors
{
    public static Error NotFound(int blogPostId) => Error.NotFound(
        "BlogPost.NotFound",
        $"The blog post with Id {blogPostId} was not found.");
}