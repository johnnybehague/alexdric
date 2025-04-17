using Alexdric.Sample.Domain.Entities;

namespace Alexdric.Sample.Domain.Repositories;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecastEntity>> GetAllWeatherForecastAsync();

    Task<WeatherForecastEntity> GetByIdWeatherForecastAsync(int id);
}
