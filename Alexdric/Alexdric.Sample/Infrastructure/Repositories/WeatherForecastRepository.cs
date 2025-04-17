using Alexdric.Sample.Application.Contexts;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Sample.Infrastructure.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly IAppDbContext _context;

    public WeatherForecastRepository(IAppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<WeatherForecastEntity>> GetAllWeatherForecastAsync()
    {
        return await _context.WeatherForecasts.ToListAsync();
    }

    public async Task<WeatherForecastEntity> GetByIdWeatherForecastAsync(int id)
    {
        var entities = await _context.WeatherForecasts.ToListAsync();
        return entities.First(x => x.Id == id);
    }
}
