using CoreProjectAPI.Models.Domain;

namespace CoreProjectAPI.Repositories.Interface;

public interface IImageRepository
{
    Task<BlogImage> Upload(IFormFile imageFile, BlogImage blogImage);
    Task<IEnumerable<BlogImage>> GetAllAsync();
}