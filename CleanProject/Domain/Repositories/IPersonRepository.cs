using Domain.Entities;
using Domain.Repositories.Generic;

namespace Domain.Repositories;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<bool> Exists(int id);
}