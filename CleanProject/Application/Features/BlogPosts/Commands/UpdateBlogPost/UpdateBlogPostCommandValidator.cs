using FluentValidation;

namespace Application.Features.BlogPosts.Commands.UpdateBlogPost;

/// <summary>
/// Validates incoming request for updating blog posts.
/// </summary>
internal sealed class UpdateBlogPostCommandValidator : AbstractValidator<UpdateBlogPostCommand>
{
    /// <summary>
    /// Creates validation rules for the blog post properties.
    /// </summary>
    public UpdateBlogPostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull();
    }
}