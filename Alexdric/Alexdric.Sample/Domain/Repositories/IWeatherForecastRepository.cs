using Alexdric.Domain.Repositories;
using Alexdric.Sample.Domain.Entities;

namespace Alexdric.Sample.Domain.Repositories;

public interface IWeatherForecastRepository : ICreateRepository<WeatherForecastEntity>
{
    Task<IEnumerable<WeatherForecastEntity>> GetAllWeatherForecastAsync();

    Task<WeatherForecastEntity> GetByIdWeatherForecastAsync(int id);
}
