using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;

namespace Application.Features.BlogPosts.Queries.GetBlogPostList;

public class GetBlogPostListQuery : IQuery<List<BlogPostDto>>;