using Application.Abstractions.Messaging;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Commands.UpdateBlogPost;

public class UpdateBlogPostCommandHandler(
    IBlogPostRepository blogPostRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateBlogPostCommand>
{
    public async Task<Result> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = await blogPostRepository.GetById(request.Id);
        if (blogPost is null)
        {
            return Result.Failure(BlogPostsErrors.NotFound(request.Id));
        }

        blogPost.Update(request.UpdateBlogPostDto.Title, request.UpdateBlogPostDto.Description);
        blogPostRepository.Update(blogPost);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}