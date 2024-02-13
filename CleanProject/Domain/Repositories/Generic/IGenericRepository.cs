using Domain.Primitives;

namespace Domain.Repositories.Generic;

public interface IGenericRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetById(int id);
    Task<IReadOnlyList<TEntity>> GetAll();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}