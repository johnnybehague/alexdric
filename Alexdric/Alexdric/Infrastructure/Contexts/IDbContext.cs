using Alexdric.Domain.Entities;

namespace Alexdric.Infrastructure.Contexts;

/// <summary>
/// Interface for DbContext
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
public interface IDbContext<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TEntity> GetEntities();
}