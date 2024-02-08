using Domain.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Generic;

public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : class
{
    public async Task<T?> Get(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> Add(T entity)
    {
        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Update(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}