using Alexdric.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Domain.Repositories;

/// <summary>
/// Interface of CreateRepository
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
public interface IQueryRepository<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns>List of entities</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <returns>List of entities</returns>
    Task<TEntity> GetByIdAsync(int id);
}