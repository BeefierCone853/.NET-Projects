namespace CoreProjectAPI.Models.DTO.BlogImage;

public record BlogImageDto(
    Guid Id,
    string FileName,
    string FileExtension,
    string Title,
    string Url,
    DateTime DateCreated);