using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Commands.CreateBlogPost;

internal sealed class CreateBlogPostCommandHandler(
    IBlogPostRepository blogPostRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICommandHandler<CreateBlogPostCommand>
{
    public async Task<Result> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = mapper.Map<BlogPost>(request.CreateBlogPostDto);
        blogPostRepository.Add(blogPost);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}