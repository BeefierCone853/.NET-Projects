using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Commands.UpdateBlogPost;

/// <summary>
/// Handler for updating blog posts.
/// </summary>
/// <param name="blogPostRepository">Repository for performing database operations on <see cref="IBlogPostRepository"/> entity.</param>
/// <param name="unitOfWork">Used to group multiple database-related operations into a single transaction.</param>
internal sealed class UpdateBlogPostCommandHandler(
    IBlogPostRepository blogPostRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateBlogPostCommand>
{
    /// <summary>
    /// Handles updating information about the blog post.
    /// </summary>
    /// <param name="request">Contains new information about the blog post.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <remarks><see cref="BlogPostsErrors.NotFound"/> is returned if the blog post doesn't exist.</remarks>
    /// <returns><see cref="Result"/> contaning information if the blog post was successfully updated or not.</returns>
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