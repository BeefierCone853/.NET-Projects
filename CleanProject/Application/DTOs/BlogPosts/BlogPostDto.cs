using Application.Abstractions;
using Domain.Primitives;

namespace Application.DTOs.BlogPosts;

public sealed record BlogPostDto(string Title, string Description, int Id) : BaseDto(Id), IBlogPostDto;