using Application.Abstractions.Messaging;
using Application.Features.BlogPosts.DTOs;

namespace Application.Features.BlogPosts.Queries.GetBlogPostById;

public sealed record GetBlogPostByIdQuery(int Id) : IQuery<BlogPostDto>;