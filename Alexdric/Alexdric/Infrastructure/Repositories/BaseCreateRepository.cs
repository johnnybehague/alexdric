using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Infrastructure.Repositories;


public class BaseCreateRepository<TEntity> : ICreateRepository<TEntity>
    where TEntity : IEntity
{

    public BaseCreateRepository() { }

    public Task<EntityState> CreateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
