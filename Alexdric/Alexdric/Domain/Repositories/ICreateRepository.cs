using Alexdric.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Domain.Repositories;

public interface ICreateRepository<TEntity>
    where TEntity : IEntity
{
    Task<EntityState> CreateAsync(TEntity entity);
}
