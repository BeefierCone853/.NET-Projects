using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

internal sealed class CreateBlogPostCommandHandler(
    IBlogPostRepository blogPostRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICommandHandler<CreateBlogPostCommand, int>
{
    public async Task<Result<int>> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = mapper.Map<BlogPost>(request.CreateBlogPostDto);
        blogPostRepository.Add(blogPost);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return blogPost.Id;
    }
}