using Domain.Entities;
using Domain.Repositories.Generic;

namespace Domain.Repositories;

public interface IBlogPostRepository : IGenericRepository<BlogPost>
{
    Task<bool> Exists(int id);
}