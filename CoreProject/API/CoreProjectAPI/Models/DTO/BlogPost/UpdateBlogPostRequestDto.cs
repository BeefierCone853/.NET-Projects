namespace CoreProjectAPI.Models.DTO.BlogPost;

public record UpdateBlogPostRequestDto(
    string Title,
    string ShortDescription,
    string Content,
    string FeaturedImageUrl,
    string UrlHandle,
    DateTime PublishedDate,
    string Author,
    bool IsVisible,
    Guid[] Categories);