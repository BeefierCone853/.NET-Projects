using FluentValidation;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

/// <summary>
/// Validates incoming request for creating blog posts.
/// <remarks>Used when <see cref="CreateBlogPostCommand"/> is sent.</remarks>
/// </summary>
internal sealed class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    /// <summary>
    /// Creates validation rules for the blog post properties.
    /// </summary>
    public CreateBlogPostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithErrorCode(BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingTitle)
            .NotNull().WithErrorCode(BlogPostErrorCodes.SharedCreateUpdateBlogPost.NullTitle);
        RuleFor(x => x.Description)
            .NotEmpty().WithErrorCode(BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingDescription)
            .NotNull().WithErrorCode(BlogPostErrorCodes.SharedCreateUpdateBlogPost.NullDescription);
    }
}