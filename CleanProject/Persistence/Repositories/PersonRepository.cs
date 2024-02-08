using Domain.Entities;
using Domain.Repositories;
using Persistence.Repositories.Generic;

namespace Persistence.Repositories;

public class PersonRepository(ApplicationDbContext dbContext) : GenericRepository<Person>(dbContext), IPersonRepository
{
    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
    }
}