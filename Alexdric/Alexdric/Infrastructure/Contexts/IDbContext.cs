using Alexdric.Domain.Entities;

namespace Alexdric.Infrastructure.Contexts;

public interface IDbContext<TEntity>
    where TEntity : IEntity
{
    public IEnumerable<TEntity> GetEntities();
}