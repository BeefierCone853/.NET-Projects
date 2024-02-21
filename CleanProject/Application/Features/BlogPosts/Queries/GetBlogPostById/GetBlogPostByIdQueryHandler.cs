using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;
using AutoMapper;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Queries.GetBlogPostById;

internal sealed class GetBlogPostByIdQueryHandler(
    IBlogPostRepository blogPostRepository,
    IMapper mapper)
    : IQueryHandler<GetBlogPostByIdQuery, BlogPostDto>
{
    public async Task<Result<BlogPostDto>> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var blogPost = await blogPostRepository.GetById(request.Id);
        if (blogPost is null)
        {
            return Result.Failure<BlogPostDto>(BlogPostsErrors.NotFound(request.Id));
        }

        var response = mapper.Map<BlogPostDto>(blogPost);
        return response;
    }
}