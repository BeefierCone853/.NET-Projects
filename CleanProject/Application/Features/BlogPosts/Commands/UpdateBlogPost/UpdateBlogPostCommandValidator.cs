using FluentValidation;

namespace Application.Features.BlogPosts.Commands.UpdateBlogPost;

internal sealed class UpdateBlogPostCommandValidator : AbstractValidator<UpdateBlogPostCommand>
{
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