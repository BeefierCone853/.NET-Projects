using FluentValidation;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

public class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    public CreateBlogPostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull();
    }
}