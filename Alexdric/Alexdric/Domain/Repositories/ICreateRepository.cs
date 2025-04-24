using Alexdric.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Domain.Repositories;

/// <summary>
/// Interface of CreateRepository
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
public interface ICreateRepository<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Create a new Entity
    /// </summary>
    /// <param name="entity">Entity to create</param>
    /// <returns>Result</returns>
    Task<EntityState> CreateAsync(TEntity entity);
}
