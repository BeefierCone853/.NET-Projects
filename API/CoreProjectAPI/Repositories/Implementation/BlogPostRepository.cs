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
        return await dbContext.
            BlogPosts.
            Include(x => x.Categories).
            ToListAsync();
    }

    public Task<BlogPost?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BlogPost?> EditAsync(BlogPost blogPost)
    {
        throw new NotImplementedException();
    }

    public Task<BlogPost?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}