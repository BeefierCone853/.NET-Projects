using CoreProjectAPI.Data;
using CoreProjectAPI.Models.Domain;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoreProjectAPI.Repositories.Implementation;

public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await dbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetById(Guid id)
    {
        return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }
}