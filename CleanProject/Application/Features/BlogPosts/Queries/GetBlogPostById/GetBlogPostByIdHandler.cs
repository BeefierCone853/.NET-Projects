using Application.Abstractions.Messaging;
using Application.DTOs.BlogPosts;
using AutoMapper;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Queries.GetBlogPostById;

internal sealed class GetBlogPostByIdHandler(
    IBlogPostRepository blogPostRepository,
    IMapper mapper)
    : IQueryHandler<GetBlogPostByIdQuery, BlogPostDto>
{
    public async Task<Result<BlogPostDto>> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var blogPost = await blogPostRepository.GetById(request.Id);
        if (blogPost is null)
        {
            return Result.Failure<BlogPostDto>(new Error(
                "BlogPost.NotFound",
                $"The blog post with Id {request.Id} was not found."));
        }

        var response = mapper.Map<BlogPostDto>(blogPost);
        return response;
    }
}