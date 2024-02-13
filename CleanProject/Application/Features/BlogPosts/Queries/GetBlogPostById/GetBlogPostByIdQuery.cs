using Application.Abstractions.Messaging;
using Application.DTOs.BlogPosts;

namespace Application.Features.BlogPosts.Queries.GetBlogPostById;

public sealed record GetBlogPostByIdQuery(int Id) : IQuery<BlogPostDto>;