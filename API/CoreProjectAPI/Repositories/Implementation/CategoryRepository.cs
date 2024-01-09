using CoreProjectAPI.Data;
using CoreProjectAPI.Models.Domain;
using CoreProjectAPI.Repositories.Interface;

namespace CoreProjectAPI.Repositories.Implementation;

public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
        return category;
    }
}