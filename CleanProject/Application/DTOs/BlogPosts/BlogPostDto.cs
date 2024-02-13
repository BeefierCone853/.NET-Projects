using Application.Abstractions;
using Domain.Primitives;

namespace Application.DTOs.BlogPosts;

public record BlogPostDto(string Title, string Description, int Id) : BaseDto(Id), IBlogPostDto;