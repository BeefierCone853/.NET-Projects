using CoreProjectAPI.Models.Domain;

namespace CoreProjectAPI.Repositories.Interface;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
}