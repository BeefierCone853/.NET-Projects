using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;
using AutoMapper;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.BlogPosts.Queries.GetBlogPostList;

internal sealed class GetBlogPostListQueryHandler(
    IBlogPostRepository blogPostRepository,
    IMapper mapper) : IQueryHandler<GetBlogPostListQuery, List<BlogPostDto>>
{
    public async Task<Result<List<BlogPostDto>>> Handle(GetBlogPostListQuery request, CancellationToken cancellationToken)
    {
        var blogPosts = await blogPostRepository.GetAll();
        var response = blogPosts.Select(blogPost => new BlogPostDto(
            Id: blogPost.Id,
            Title: blogPost.Title,
            Description: blogPost.Description)).ToList();
        return response;
    }
}