namespace CoreProjectAPI.Models.DTO.BlogPost;

public record BlogPostDto(
    Guid Id,
    string Title,
    string ShortDescription,
    string Content,
    string FeaturedImageUrl,
    string UrlHandle,
    DateTime PublishedDate,
    string Author,
    bool IsVisible,
    List<CategoryDto> Categories);