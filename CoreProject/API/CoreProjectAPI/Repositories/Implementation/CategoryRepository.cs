using System.Formats.Asn1;
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

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category?> EditAsync(Category category)
    {
        var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
        if (existingCategory is null) return null;
        dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
        await dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> DeleteAsync(Guid id)
    {
        var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (existingCategory is null)
        {
            return null;
        }
        dbContext.Categories.Remove(existingCategory);
        await dbContext.SaveChangesAsync();
        return existingCategory;
    }
}