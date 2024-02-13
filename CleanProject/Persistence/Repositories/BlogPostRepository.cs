using Domain.Entities;
using Domain.Repositories;
using Persistence.Repositories.Generic;

namespace Persistence.Repositories;

public class BlogPostRepository(ApplicationDbContext dbContext) : GenericRepository<BlogPost>(dbContext), IBlogPostRepository
{
    public async Task<bool> Exists(int id)
    {
        var entity = await GetById(id);
        return entity != null;
    }
}