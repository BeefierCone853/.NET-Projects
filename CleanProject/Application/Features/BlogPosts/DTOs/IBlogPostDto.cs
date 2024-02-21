namespace Application.Features.BlogPosts.DTOs;

public interface IBlogPostDto
{
    string Title { get; init; }
    string Description { get; init; }
}