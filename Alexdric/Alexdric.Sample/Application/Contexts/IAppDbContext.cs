using Alexdric.Sample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Sample.Application.Contexts;

public interface IAppDbContext
{
    DbSet<WeatherForecastEntity> WeatherForecasts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
