using Domain.Primitives;

namespace Application.Features.BlogPosts.DTOs;

public sealed record BlogPostDto(string Title, string Description, int Id) : BaseDto(Id), IBlogPostDto;