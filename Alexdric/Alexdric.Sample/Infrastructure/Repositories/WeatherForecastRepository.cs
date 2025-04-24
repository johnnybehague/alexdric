using Alexdric.Sample.Application.Contexts;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Sample.Infrastructure.Repositories;

/// <summary>
/// Repository for the WeatherForecast data
/// </summary>
public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Initialize a new instance of the class WeatherForecastRepository
    /// </summary>
    /// <param name="context">Application Database Context</param>
    /// <exception cref="ArgumentNullException"></exception>
    public WeatherForecastRepository(IAppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Get All Weatherforecasts
    /// </summary>
    /// <returns>List of Weatherforecast entities</returns>
    public async Task<IEnumerable<WeatherForecastEntity>> GetAllWeatherForecastAsync()
    {
        return await _context.WeatherForecasts.ToListAsync();
    }

    /// <summary>
    /// Get Weatherforecast by Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Weatherforecast with specified id</returns>
    public async Task<WeatherForecastEntity> GetByIdWeatherForecastAsync(int id)
    {
        var entities = await _context.WeatherForecasts.ToListAsync();
        return entities.First(x => x.Id == id);
    }

    /// <summary>
    /// Create a new Weatherforecast
    /// </summary>
    /// <param name="entity">Weatherforecast Entity</param>
    /// <returns>Result</returns>
    public async Task<EntityState> CreateAsync(WeatherForecastEntity entity)
    {
        await _context.WeatherForecasts.AddAsync(entity);
        var result = await _context.SaveChangesAsync(CancellationToken.None);
        return result > 0 ? EntityState.Added : EntityState.Unchanged;
    }
}
