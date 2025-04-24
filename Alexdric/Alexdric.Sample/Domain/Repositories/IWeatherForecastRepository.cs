using Alexdric.Domain.Repositories;
using Alexdric.Sample.Domain.Entities;

namespace Alexdric.Sample.Domain.Repositories;

/// <summary>
/// Repository interface for the WeatherForecast data
/// </summary>
public interface IWeatherForecastRepository : ICreateRepository<WeatherForecastEntity>
{
    /// <summary>
    /// Get All Weatherforecasts
    /// </summary>
    /// <returns>List of Weatherforecast entities</returns>
    Task<IEnumerable<WeatherForecastEntity>> GetAllWeatherForecastAsync();

    /// <summary>
    /// Get Weatherforecast by Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Weatherforecast with specified id</returns>
    Task<WeatherForecastEntity> GetByIdWeatherForecastAsync(int id);
}
