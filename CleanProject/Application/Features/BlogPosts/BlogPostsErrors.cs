using System.Runtime.InteropServices.JavaScript;
using Domain.Shared;

namespace Application.Features.BlogPosts;

public static class BlogPostsErrors
{
    public static Error NotFound(int blogPostId) => Error.NotFound(
        "BlogPost.NotFound",
        $"The blog post with Id {blogPostId} was not found.");
}