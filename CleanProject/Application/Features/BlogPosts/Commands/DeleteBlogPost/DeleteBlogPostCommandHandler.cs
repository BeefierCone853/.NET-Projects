using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Commands.DeleteBlogPost;

/// <summary>
/// Handler for deleting blog posts.
/// </summary>
/// <param name="blogPostRepository">Repository for performing database operations on <see cref="IBlogPostRepository"/> entity.</param>
/// <param name="unitOfWork">Used to group multiple database-related operations into a single transaction.</param>
internal sealed class DeleteBlogPostCommandHandler(
    IBlogPostRepository blogPostRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteBlogPostCommand>
{
    /// <summary>
    /// Handles the deletion of the blog post.
    /// </summary>
    /// <param name="request">Contains the ID of the blog post to be deleted.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <remarks><see cref="BlogPostsErrors.NotFound"/> is returned if the blog post doesn't exist.</remarks>
    /// <returns><see cref="Result"/> contaning information if the delete was successful or not.</returns>
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