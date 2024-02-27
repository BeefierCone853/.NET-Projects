namespace Application.Features.BlogPosts;

/// <summary>
/// Error codes for blog post validators. 
/// </summary>
public static class BlogPostErrorCodes
{
    /// <summary>
    /// Shared error codes for creating and updating blog posts.
    /// </summary>
    public static class SharedCreateUpdateBlogPost
    {
        public const string MissingTitle = nameof(MissingTitle);
        public const string NullTitle = nameof(NullTitle);
        public const string MissingDescription = nameof(MissingDescription);
        public const string NullDescription = nameof(NullDescription);
    }

    public static class BlogPostPagination
    {
        public const string PageGreaterThanZero = nameof(PageGreaterThanZero);
    }
}