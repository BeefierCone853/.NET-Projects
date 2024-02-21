using Application.Abstractions.Messaging;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Commands.DeleteBlogPost;

internal sealed class DeleteBlogPostCommandHandler(
    IBlogPostRepository blogPostRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteBlogPostCommand>
{
    public async Task<Result> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = await blogPostRepository.GetById(request.BlogPostId);
        if (blogPost is null)
        {
            return Result.Failure(BlogPostsErrors.NotFound(request.BlogPostId));
        }

        blogPostRepository.Delete(blogPost);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}