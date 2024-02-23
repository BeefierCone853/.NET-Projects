using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;

namespace Application.Features.BlogPosts.Queries.GetBlogPostList;

/// <summary>
/// Query for retrieving all blog posts.
/// </summary>
public class GetBlogPostListQuery : IQuery<List<BlogPostDto>>;