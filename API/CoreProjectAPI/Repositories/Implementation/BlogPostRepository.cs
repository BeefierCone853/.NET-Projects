using CoreProjectAPI.Data;
using CoreProjectAPI.Models.Domain;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoreProjectAPI.Repositories.Implementation;

public class BlogPostRepository(ApplicationDbContext dbContext) : IBlogPostRepository
{
    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
        await dbContext.BlogPosts.AddAsync(blogPost);
        await dbContext.SaveChangesAsync();
        return blogPost;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await dbContext
            .BlogPosts
            .Include(x => x.Categories)
            .ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(Guid id)
    {
        return await dbContext
            .BlogPosts
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost?> EditAsync(BlogPost blogPost)
    {
        var existingBlogPost = await dbContext.BlogPosts
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id == blogPost.Id);
        if (existingBlogPost is null)
        {
            return null;
        }
        dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
        existingBlogPost.Categories = blogPost.Categories;
        await dbContext.SaveChangesAsync();
        return blogPost;
    }

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var existingBlogPost = await dbContext.BlogPosts
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (existingBlogPost is null)
        {
            return null;
        }

        dbContext.BlogPosts.Remove(existingBlogPost);
        await dbContext.SaveChangesAsync();
        return existingBlogPost;
    }
}