using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Infrastructure.Repositories;


/// <summary>
/// Base Create Repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseCreateRepository<TEntity> : ICreateRepository<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Initialize a new instance of BaseCreateRepository
    /// </summary>
    public BaseCreateRepository() { }

    /// <summary>
    /// Create a new Entity
    /// </summary>
    /// <param name="entity">Entity to create</param>
    /// <returns>Result</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<EntityState> CreateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
