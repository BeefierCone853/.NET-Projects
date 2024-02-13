namespace Application.Abstractions;

public interface IBlogPostDto
{
    string Title { get; init; }
    string Description { get; init; }
}