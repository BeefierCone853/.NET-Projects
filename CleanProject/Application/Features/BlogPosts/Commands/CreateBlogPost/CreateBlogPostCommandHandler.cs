using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Features.BlogPosts;
using Domain.Shared;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

/// <summary>
/// Handler for creating blog posts.
/// </summary>
/// <param name="blogPostRepository">Repository for performing database operations on <see cref="IBlogPostRepository"/> entity.</param>
/// <param name="unitOfWork">Used to group multiple database-related operations into a single transaction.</param>
/// <param name="mapper">Maps incoming request to an entity.</param>
internal sealed class CreateBlogPostCommandHandler(
    IBlogPostRepository blogPostRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICommandHandler<CreateBlogPostCommand, int>
{
    /// <summary>
    /// Handles the creation of the blog post.
    /// </summary>
    /// <param name="request">Contains information about the new blog post.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns><see cref="Result"/> containing a unique identifier of the newly created blog post.</returns>
    public async Task<Result<int>> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = mapper.Map<BlogPost>(request.CreateBlogPostDto);
        blogPostRepository.Add(blogPost);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return blogPost.Id;
    }
}