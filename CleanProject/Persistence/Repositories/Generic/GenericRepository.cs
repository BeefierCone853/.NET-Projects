using Domain.Primitives;
using Domain.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Generic;

internal abstract class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity>
    where TEntity : Entity
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    public virtual async Task<TEntity?> GetById(int id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAll()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }

    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}