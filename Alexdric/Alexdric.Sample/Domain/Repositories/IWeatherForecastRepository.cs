using Alexdric.Domain.Repositories;
using Alexdric.Sample.Domain.Entities;

namespace Alexdric.Sample.Domain.Repositories;

/// <summary>
/// Repository interface for the WeatherForecast data
/// </summary>
public interface IWeatherForecastRepository : 
    ICreateRepository<WeatherForecastEntity>, 
    IQueryRepository<WeatherForecastEntity>
{
}
