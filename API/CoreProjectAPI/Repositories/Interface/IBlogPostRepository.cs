using CoreProjectAPI.Models.Domain;

namespace CoreProjectAPI.Repositories.Interface;

public interface IBlogPostRepository
{
    Task<BlogPost> CreateAsync(BlogPost category);
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(Guid id);
    Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
    Task<BlogPost?> EditAsync(BlogPost blogPost);
    Task<BlogPost?> DeleteAsync(Guid id);
}